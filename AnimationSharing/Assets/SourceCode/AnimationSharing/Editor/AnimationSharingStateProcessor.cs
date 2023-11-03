using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace AnimationSharing
{
    [Serializable]
    public class AnimationSharingStateProcessor : ScriptableObject
    {
        public AnimationStateEnum GetAnimationStateEnum()
        {
            return GetAnimationStateEnum_Internal();
        }

        public void ProcessActorStates()
        {
            ProcessActorStates_Internal();
        }

        protected virtual AnimationStateEnum GetAnimationStateEnum_Internal()
        {
            return null;
        }

        protected virtual void ProcessActorStates_Internal()
        {

        }
    }
}
