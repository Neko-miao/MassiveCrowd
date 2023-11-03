using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AnimationSharing
{
    [Serializable]
    public class AnimationSharingSetupWithAvatar
    {
        public Avatar Skeletal;
        public Mesh SkinnedMesh;
        public AnimationSharingStateProcessor StateProcessor;
        public MonoBehaviour ssss;
        public AnimationStateEntry[] AnimStates;
    }

    [Serializable]
    public class AnimationStateEntry
    {
        public uint State;
        public AnimationStateSetup[] StateSetups;
    }

    [Serializable]
    public class AnimationStateSetup
    {
        public AnimationClip AnimClip;
    }
}
