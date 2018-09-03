using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelCave1Playable : PlayableAsset
{

    public ExposedReference<GameObject> playerGirl;
    private Animator girl;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var scriptPlayable = ScriptPlayable<LevelCave1Ctrl>.Create(graph);
        girl = playerGirl.Resolve(graph.GetResolver()).GetComponent<Animator>();
        scriptPlayable.GetBehaviour().girl = girl;
        return scriptPlayable;
    }

    
}
