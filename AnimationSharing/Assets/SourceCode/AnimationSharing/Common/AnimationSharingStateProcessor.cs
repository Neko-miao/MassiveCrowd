using AnimationSharing;
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
    public class AnimationSharingStateProcessor : ScriptableObject
    {
        public AnimationStateEnum GetAnimationStateEnum()
        {
            return GetAnimationStateEnum_Internal();
        }

        public int ProcessActorStates(GameObject actor)
        {
            return ProcessActorStates_Internal(actor);
        }

        protected virtual AnimationStateEnum GetAnimationStateEnum_Internal()
        {
            return null;
        }

        protected virtual int ProcessActorStates_Internal(GameObject actor)
        {
            return 0;
        }
    }
}
