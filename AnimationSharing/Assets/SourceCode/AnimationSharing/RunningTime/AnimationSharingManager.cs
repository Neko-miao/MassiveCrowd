using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace AnimationSharing
{
    public class AnimationSharingManager
    {
        private AnimationSharingInstance mAnimSharingInstance;
        private Dictionary<ulong, GameObject> mRegisteredActors;
        private Avatar mSkeleton;

        internal AnimationSharingManager() { }

        public void Initialise(AnimationSharingSetup setup)
        {
            mSkeleton = setup.SetupWithAvatar.Skeleton;
            if(mSkeleton != null)
            {
                mAnimSharingInstance = new AnimationSharingInstance();
                mAnimSharingInstance.Setup(setup.SetupWithAvatar);
            }
            else
            {
                UnityEngine.Debug.LogError("Avatar is null.");
            }
        }

        public void OnDestroyed()
        {

        }

        public void RegisterActorWithAvatar(GameObject actor)
        {

        }

        public void UnregisterActor(GameObject actor)
        {

        }

        public void Tick(float deltaTime)
        {
            if(mAnimSharingInstance != null)
            {
                mAnimSharingInstance.TickActorStates();
                mAnimSharingInstance.KickoffInstances();
                mAnimSharingInstance.TickAnimationStates();
            }
        }
    }
}
