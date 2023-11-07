using AnimationSharing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public AnimationSharingSetup Setup;


    private AnimationSharingManager mAnimSharingManager;
    private List<GameObject> TestActors;
    
    void Start()
    {
        // 创建AnimationSharingManager        
        mAnimSharingManager = AnimationSharingModule.CreateAnimationSharingManager(Setup);

        // 创建若干个Actor
        Test_CreateSharingActors();

        // 注册这些Actor
        Test_RegisterAllSharingActors();
    }


    void Update()
    {
        if (mAnimSharingManager != null)
        {
            mAnimSharingManager.Tick(UnityEngine.Time.deltaTime);
        }
    }


    private void Test_CreateSharingActors()
    {
        Vector3 origin = new Vector3(0f, 0f, 0f);
        const float xOffset = 1.5f;
        const float zOffset = 1.5f;
        const int ActorCount = 100;
        const int MaxRowCount = 20;

        for(int i = 0; i < ActorCount; i++)
        {
            Vector3 pos = new Vector3((i % MaxRowCount) * xOffset, 0.0f, (i / MaxRowCount) * zOffset);
            CreateSharingActor(pos);
        }

    }

    private void CreateSharingActor(Vector3 createPos)
    {

    }

    private void Test_RegisterAllSharingActors()
    {
        foreach(var actor in TestActors)
        {
            mAnimSharingManager.RegisterActorWithAvatar(actor);
        }
    }
}
