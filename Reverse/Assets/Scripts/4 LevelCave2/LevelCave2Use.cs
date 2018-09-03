using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelCave2Use : PlayableBehaviour
{
    public Animator girl;
    
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        base.OnBehaviourPlay(playable, info);
        girl.gameObject.GetComponent<BoyController>().setAttack();
    }
}
