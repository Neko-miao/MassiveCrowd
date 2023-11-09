using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class PlayableTest : MonoBehaviour
{
    public AnimationClip clip;
    public GameObject prefab;

    private PlayableGraph graph;
    // Start is called before the first frame update
    void Start()
    {
        GameObject actor = GameObject.Instantiate(prefab);
        actor.transform.position = Vector3.zero;
        Animator animator = actor.GetComponent<Animator>();
        graph = PlayableGraph.Create("PlayableTest");
        graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        AnimationPlayableOutput output = AnimationPlayableOutput.Create(graph, "AnimationOutput", animator);
        AnimationClipPlayable clipPlayable = AnimationClipPlayable.Create(graph, clip);
        output.SetSourcePlayable(clipPlayable);
        graph.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        graph.Destroy();
    }
}
