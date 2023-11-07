using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AnimationSharing
{
    internal class AnimationSharingInstance
    {
        private List<PerActorData> mPerActorData;
        private List<PerStateData> mPerStateData;
        private AnimationSharingManager mAnimSharingManager;
        private Avatar Skeleton;
        private AnimationSharingStateProcessor mStateProcessor;

        internal AnimationSharingInstance() { }

        public void Setup(AnimationSharingSetupWithAvatar setup)
        {
            Skeleton = setup.Skeleton;
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

                    // 为每个Clip构造Playable动画
                }
                mPerStateData.Add(data);
            }
        }

        public void TickActorStates()
        {
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

                    actorData.PreviousState = previousState;
                    actorData.CurrentState = currentState;
                }
            }
        }

        public void KickoffInstances()
        {

        }

        public void TickAnimationStates()
        {
            
        }

        public void TickAnimation()
        {
            // 这里用Playable的方式来播放动画
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
            var count = stateData.SkinnedMeshes.Length;
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

            foreach(var skinnedMesh in actorData.SkinnedMeshes)
            {
                // TODO: 这里通过设置rootbone的方式来“共享”动画，后续可能会在引擎层面用另一种方式实现
                skinnedMesh.rootBone = skinnedMeshRenderer.rootBone;
            }
        }
    }
}
