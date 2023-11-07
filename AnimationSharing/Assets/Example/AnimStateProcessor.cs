using AnimationSharing;
using UnityEngine;

public class AnimStateProcessor : AnimationSharingStateProcessor
{
    protected override AnimationStateEnum GetAnimationStateEnum_Internal()
    {
        return AnimStateEnum.Instance;
    }

    protected override int ProcessActorStates_Internal(GameObject actor)
    {
        var comp = actor.GetComponent<AnimationSharingComponent>();
        if(comp != null)
        {
            return comp.GetCurrentState();
        }
        return 0;
    }
}
