using AnimationSharing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class GameController : MonoBehaviour
{
    public Vector3 origin = new Vector3(0f, 0f, 0f);
    public  float xOffset = 1.5f;
    public  float zOffset = -1.5f;
    public  int ActorCount = 100;
    public  int MaxRowCount = 20;

    public AnimationSharingSetup Setup;
    public GameObject SharingActor;

    private AnimationSharingManager mAnimSharingManager;
    private List<GameObject> TestActors = new List<GameObject>();
    
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
        Profiler.BeginSample("GameController");
        if (mAnimSharingManager != null)
        {
            mAnimSharingManager.Tick(UnityEngine.Time.deltaTime);
        }
        Profiler.EndSample();
    }


    private void Test_CreateSharingActors()
    {
        for(int i = 0; i < ActorCount; i++)
        {
            Vector3 pos = new Vector3((i % MaxRowCount) * xOffset, 0.0f, (i / MaxRowCount) * zOffset);
            CreateSharingActor(pos);
        }

    }

    private void CreateSharingActor(Vector3 createPos)
    {
        GameObject actor = GameObject.Instantiate(SharingActor);
        actor.name = string.Format("TestActor_{0}", TestActors.Count);
        actor.transform.position = createPos;
        TestActors.Add(actor);
    }

    private void Test_RegisterAllSharingActors()
    {
        foreach(var actor in TestActors)
        {
            mAnimSharingManager.RegisterActorWithAvatar(actor);
        }
    }
}
