using AnimationSharing;

public class AnimStateEnum : AnimationStateEnum
{
    private static AnimStateEnum mInstance;

    private AnimStateEnum() { }

    public static AnimStateEnum Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = new AnimStateEnum();
            }
            return mInstance;
        }
    }
}
