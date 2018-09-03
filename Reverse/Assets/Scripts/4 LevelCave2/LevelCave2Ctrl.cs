using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelCave2Ctrl : PlayableBehaviour
{

    public Animator girl;

    public override void OnGraphStart(Playable playable)
    {
        girl.gameObject.GetComponent<BoyController>().ctrlState = BoyController.CtrlState.NonPlayer;
        base.OnGraphStart(playable);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        base.PrepareFrame(playable, info);
        girl.gameObject.GetComponent<BoyController>().setVelocity();
    }

    public override void OnGraphStop(Playable playable)
    {
        if (girl != null)
            girl.gameObject.GetComponent<BoyController>().ctrlState = BoyController.CtrlState.Player;
        base.OnGraphStop(playable);
        
    }
}
