using UnityEngine;

namespace AnimationSharing
{
    public class AnimationSharingModule
    {
        private static AnimationSharingManager AnimSharingManager;
        public static AnimationSharingManager CreateAnimationSharingManager(AnimationSharingSetup setup)
        {
            if(AnimSharingManager == null)
            {
                AnimSharingManager = new AnimationSharingManager();
                AnimSharingManager.Initialise(setup);
            }

            return AnimSharingManager;
        }

        public AnimationSharingManager GetAnimationSharingManager()
        {
            if(AnimSharingManager != null)
            {
                return AnimSharingManager;
            }

            Debug.LogFormat("AnimSharingManager is null");
            return null;
        }

        public void OnApplicationDestroyed()
        {
            AnimSharingManager.OnDestroyed();
        }
    }
}
