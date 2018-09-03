using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelGreenLandPlayable : PlayableAsset
{


    public ExposedReference<GameObject> playerGirl;
    public ExposedReference<GameObject> playerBoy;
    private Animator girl;
    private Animator boy;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var scriptPlayable = ScriptPlayable<LevelGreenLandCtrl>.Create(graph);
        girl = playerGirl.Resolve(graph.GetResolver()).GetComponent<Animator>();
        boy = playerBoy.Resolve(graph.GetResolver()).GetComponent<Animator>();
        scriptPlayable.GetBehaviour().girl = girl;
        scriptPlayable.GetBehaviour().boy = boy;
        return scriptPlayable;
    }
}
