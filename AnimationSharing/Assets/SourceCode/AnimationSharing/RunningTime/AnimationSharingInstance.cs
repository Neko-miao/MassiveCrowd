using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Profiling;

namespace AnimationSharing
{
    internal class AnimationSharingInstance
    {
        private Vector3 DebugPosition = new Vector3(0f, 0f, 15f);
        private List<PerActorData> mPerActorData;
        private List<PerStateData> mPerStateData;
        private AnimationSharingManager mAnimSharingManager;
        private Avatar mSkeleton;
        private GameObject mSharedActor;
        private AnimationSharingStateProcessor mStateProcessor;

        internal AnimationSharingInstance() { }

        public void Setup(AnimationSharingSetupWithAvatar setup)
        {
            mSkeleton = setup.Skeleton;
            mSharedActor = setup.SharedPrefab;
            var processorCls = setup.StateProcessor.GetClass();
            mStateProcessor = System.Activator.CreateInstance(processorCls) as AnimationSharingStateProcessor;
            if(mStateProcessor != null )
            {
                SetupState(setup);
            }
            else
            {
                UnityEngine.Debug.LogError("AnimationSharingStateProcessor is null");
            }
        }

        private void SetupState(AnimationSharingSetupWithAvatar setup)
        {
            if(mPerStateData == null)
            {
                mPerStateData = new List<PerStateData>();
            }

            foreach(var entry in setup.AnimStates)
            {
                PerStateData data = new PerStateData();
                data.StateEnumValue = entry.State;
                for(int i = 0; i < entry.StateSetups.Length; i++)
                {
                    var stateSetup = entry.StateSetups[i];
                    if (!stateSetup.enable) continue;

                    // 设置SharedActor的SkinnedMeshRenderer
                    GameObject sharedActor = GameObject.Instantiate(mSharedActor);
                    Animator animator = sharedActor.GetComponent<Animator>();
                    SkinnedMeshRenderer skinnedMesh = sharedActor.GetComponentInChildren<SkinnedMeshRenderer>();
                    Bounds skinnedMeshBounds = skinnedMesh.bounds;
                    sharedActor.name = string.Format("SharedActor_{0}_{1}", entry.State, i);
                    sharedActor.transform.position = DebugPosition + new Vector3(skinnedMeshBounds.extents.y * 2 * i, 0f, -skinnedMeshBounds.extents.x * 2 * entry.State);
                    skinnedMesh.updateWhenOffscreen = true;
                    BonePoseBuff poseBuff = null;
                    AnimationSharingUtil.SetBonePoseBuffByRootBone(skinnedMesh.rootBone, out poseBuff);

                    Debug.LogFormat("rootBone:{0}", poseBuff.PosedBones.Count);

                    // 构造Playable用于播放动画
                    PlayableGraph graph = PlayableGraph.Create(string.Format("ShredActorPlayableGraph_{0}_{1}", entry.State, i));
                    AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "AnimationOutput", animator);
                    AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(graph, stateSetup.AnimClip);
                    output.SetSourcePlayable(clipPlayable);
                    graph.Play();

                    data.SkinnedMeshes.Add(skinnedMesh);
                    data.SharedActors.Add(sharedActor);
                    data.PlayableGraphs.Add(graph);
                    data.SharedPoseBuffs.Add(poseBuff);
                }
                mPerStateData.Add(data);
            }
        }


        public void RegisterActor(GameObject actor)
        {
            if(mPerActorData == null)
            {
                mPerActorData = new List<PerActorData>();
            }
            PerActorData data = new PerActorData();
            var skinnedMeshComp = actor.GetComponentInChildren<SkinnedMeshRenderer>();
            data.Actor = actor;
            data.SkinnedMesh = skinnedMeshComp;
            data.PreviousState = -1;
            data.CurrentState = DeterminStateForActor(data);

            var animator = actor.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = false;
            }

            data.SharedRootBone = null;
            AnimationSharingUtil.SetBonePoseBuffByRootBone(skinnedMeshComp.rootBone, out data.ActorPose);

            mPerActorData.Add(data);
        }

        public void TickActorStates()
        {
            Profiler.BeginSample("TickActorStates");
            foreach(var actorData in mPerActorData)
            {
                // TODO ： 暂时默认所有Actor都需要Tick
                actorData.IsRequiredTick = true;

                // 处理Actor的动画状态
                int previousState = actorData.PreviousState;
                int currentState = DeterminStateForActor(actorData);
                
                if(previousState != currentState)
                {
                    SetupFollowerComponent(mPerStateData[currentState], actorData);

                    actorData.PreviousState = currentState;
                    actorData.CurrentState = currentState;
                }
            }
            Profiler.EndSample();
        }

        public void KickoffInstances()
        {

        }

        public void TickAnimationStates()
        {
            
        }

        public void TickAnimation(float deltaTime)
        {
            Profiler.BeginSample("TickAnimation");
            // TODO : 这里用Playable的方式来播放动画

            // 复制SharedRootBone;
            foreach (var actorData in mPerActorData)
            {
                int currentState = actorData.CurrentState;
                int permutation = actorData.PermutationIndex;
                AnimationSharingUtil.CopyBonePosedBuff(mPerStateData[currentState].SharedPoseBuffs[permutation], actorData.ActorPose);
            }
            Profiler.EndSample();
        }

        private int DeterminStateForActor(PerActorData actorData)
        {
            if(mStateProcessor != null)
            {
                return mStateProcessor.ProcessActorStates(actorData.Actor);
            }

            return 0;
        }


        /// <summary>
        /// actor的SkinnedMeshRender组件将由stateData中的某个SkinnedMeshRender组件决定
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="actorData"></param>
        private void SetupFollowerComponent(PerStateData stateData, PerActorData actorData)
        {
            int permutationIndex = DeterminPermutationIndex(stateData, actorData);
            SerPermutationFollowerComponent(stateData, actorData, permutationIndex);
        }


        /// <summary>
        /// 决定共享stateData的哪一个SkinnedMeshRender组件
        /// </summary>
        /// <param name="stateData"></param>
        /// <param name="actorData"></param>
        /// <returns></returns>
        private int DeterminPermutationIndex(PerStateData stateData, PerActorData actorData)
        {
            // 这里随机选一个
            var count = stateData.SkinnedMeshes.Count;
            var permutation = UnityEngine.Random.Range(0, count);
            return permutation;
        }


        private void SerPermutationFollowerComponent(PerStateData stateData, PerActorData actorData, int permutationIndex)
        {
            SetLeaderComponentForActor(actorData, stateData.SkinnedMeshes[permutationIndex]);
            actorData.PermutationIndex = permutationIndex;
        }


        private void SetLeaderComponentForActor(PerActorData actorData, SkinnedMeshRenderer skinnedMeshRenderer)
        {
            if(!skinnedMeshRenderer.enabled)
            {
                skinnedMeshRenderer.enabled = true;
            }

            // TODO: 这里通过设置rootbone的方式来“共享”动画，后续可能会在引擎层面用另一种方式实现
            actorData.SharedRootBone = skinnedMeshRenderer.rootBone;
            // actorData.SkinnedMesh.rootBone = skinnedMeshRenderer.rootBone;

        }
    }
}
