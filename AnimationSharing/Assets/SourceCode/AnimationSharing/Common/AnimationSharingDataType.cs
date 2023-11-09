using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Playables;

namespace AnimationSharing
{
    public class PerActorData
    {
        public int CurrentState;
        public int PreviousState;
        public int PermutationIndex;

        public bool IsRequiredTick;

        public GameObject Actor;
        public SkinnedMeshRenderer SkinnedMesh;
        public Transform SharedRootBone;
        public BonePoseBuff ActorPose;
    }

    public class PerStateData
    {
        public int StateEnumValue;
        public List<SkinnedMeshRenderer> SkinnedMeshes = new List<SkinnedMeshRenderer>();
        public List<GameObject> SharedActors = new List<GameObject>();   // 这里因为unity的MonoBehaviour不能单独tick
        public List<PlayableGraph> PlayableGraphs = new List<PlayableGraph>(); // 用PlayableGraph来驱动动画而不是Animator
        public List<BonePoseBuff> SharedPoseBuffs = new List<BonePoseBuff>();
    }

    public class BonePoseBuff
    {
        public Transform RootBone;
        public List<Transform> PosedBones;
    }
}
