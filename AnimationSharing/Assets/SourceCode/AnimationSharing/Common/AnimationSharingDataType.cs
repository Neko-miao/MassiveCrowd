using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AnimationSharing
{
    public class PerActorData
    {
        public ulong ActorInstanceId;
        public int CurrentState;
        public int PreviousState;
        public int PermutationIndex;

        public bool IsRequiredTick;

        public GameObject Actor;
        public SkinnedMeshRenderer[] SkinnedMeshes;
    }

    public class PerStateData
    {
        public int StateEnumValue;
        public SkinnedMeshRenderer[] SkinnedMeshes;
    }
}
