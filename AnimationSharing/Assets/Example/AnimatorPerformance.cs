using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class AnimatorPerformance : MonoBehaviour
{
    public Vector3 origin = new Vector3(0f, 0f, 0f);
    public  float xOffset = 1.5f;
    public  float zOffset = -1.5f;
    public  int ActorCount = 100;
    public  int MaxRowCount = 20;

    public GameObject SharingActor;
    public AnimationClip clip;

    private List<GameObject> TestActors = new List<GameObject>();

    void Start()
    {
        // 创建若干个Actor
        Test_CreateSharingActors();

        // 用Playable运行动画
        Test_PlayAnimationClip();
    }


    void Update()
    {

    }


    private void Test_CreateSharingActors()
    {
        for (int i = 0; i < ActorCount; i++)
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

    private void Test_PlayAnimationClip()
    {
        foreach(GameObject actor in TestActors)
        {
            Animator animator = actor.GetComponent<Animator>();
            var graph = PlayableGraph.Create("PlayableTest");
            graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            var output = AnimationPlayableOutput.Create(graph, "AnimationOutput", animator);
            var clipPlayable = AnimationClipPlayable.Create(graph, clip);
            output.SetSourcePlayable(clipPlayable);
            graph.Play();
        }

    }
}
