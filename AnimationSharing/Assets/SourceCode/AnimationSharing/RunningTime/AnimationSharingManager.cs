using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace AnimationSharing
{
    internal class AnimationSharingManager
    {
        private AnimationSharingInstance mAnimSharingInstance;
        private Dictionary<ulong, GameObject> mRegisteredActors;

        private AnimationSharingManager() { }

        private static AnimationSharingManager mInstance;
        public static AnimationSharingManager Instance
        {
            get
            {
                if(mInstance == null)
                {
                    mInstance = new AnimationSharingManager();
                }
                return mInstance;
            }
        }

        public void Setup()
        {

        }

        public void RegisterActorWithAvatar(GameObject actor)
        {

        }

        public void UnregisterActor(GameObject actor)
        {

        }

        public void Tick()
        {
            if(mAnimSharingInstance != null)
            {

            }
        }
    }
}
