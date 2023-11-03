using UnityEngine;

namespace AnimationSharing
{
    [CreateAssetMenu(fileName = "AnimationSharingSetup", menuName = "AnimationSharing/AnimationSharingSetup", order = 1)]
    public class AnimationSharingSetup : ScriptableObject
    {
        [SerializeField]
        public AnimationSharingSetupWithAvatar SetupConfig;
    }
}

 