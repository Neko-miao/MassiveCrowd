using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace AnimationSharing
{
    public class AnimationSharingManager
    {
        private AnimationSharingInstance mAnimSharingInstance;
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
            // 检查avatar是否兼容  
            var animator = actor.GetComponent<Animator>();
            if(animator == null || animator.avatar != mSkeleton)
            {
                UnityEngine.Debug.LogFormat("imcompitable avatar...");
                return;
            }
            mAnimSharingInstance.RegisterActor(actor);    
        }

        public void UnregisterActor(GameObject actor)
        {
            // 恢复rootBone和Animator的设置
        }

        public void Tick(float deltaTime)
        {
            if(mAnimSharingInstance != null)
            {
                mAnimSharingInstance.TickActorStates();
                mAnimSharingInstance.KickoffInstances();
                mAnimSharingInstance.TickAnimationStates();
                mAnimSharingInstance.TickAnimation(deltaTime);
            }
        }
    }
}
