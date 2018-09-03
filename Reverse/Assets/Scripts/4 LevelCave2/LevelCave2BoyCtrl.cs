using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelCave2BoyCtrl : PlayableBehaviour
{
    public Animator boy;
    public override void OnGraphStart(Playable playable)
    {
        boy.gameObject.GetComponent<BoyController>().ctrlState = BoyController.CtrlState.NonPlayer;
        base.OnGraphStart(playable);

    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        base.PrepareFrame(playable, info);
        boy.gameObject.GetComponent<BoyController>().setVelocity();
    }

    public override void OnGraphStop(Playable playable)
    {
        if (boy != null)
            boy.gameObject.GetComponent<BoyController>().ctrlState = BoyController.CtrlState.Player;
        base.OnGraphStop(playable);

    }
}
