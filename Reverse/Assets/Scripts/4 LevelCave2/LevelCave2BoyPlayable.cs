using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelCave2BoyPlayable : PlayableAsset
{
    public ExposedReference<GameObject> playerBoy;
    private Animator boy;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var scriptPlayable = ScriptPlayable<LevelCave2BoyCtrl>.Create(graph);
        boy = playerBoy.Resolve(graph.GetResolver()).GetComponent<Animator>();
        scriptPlayable.GetBehaviour().boy = boy;
        return scriptPlayable;
    }
}
