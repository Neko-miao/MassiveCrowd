using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace AnimationSharing
{
    [Serializable]
    public class AnimationSharingSetupWithAvatar
    {
        public Avatar Skeleton;
        public MonoScript StateProcessor;
        public AnimationStateEntry[] AnimStates;
    }

    [Serializable]
    public class AnimationStateEntry
    {
        public int State;
        public AnimationStateSetup[] StateSetups;
    }

    [Serializable]
    public class AnimationStateSetup
    {
        public AnimationClip AnimClip;
        public bool enable;
    }
}
