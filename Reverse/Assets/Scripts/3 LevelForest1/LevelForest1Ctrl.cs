using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelForest1Ctrl : PlayableBehaviour
{

    public Animator girl;

    public override void OnGraphStart(Playable playable)
    {
        girl.gameObject.GetComponent<BoyController>().ctrlState = BoyController.CtrlState.NonPlayer;
        base.OnGraphStart(playable);
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
        girl.gameObject.GetComponent<BoyController>().setVelocity();
        base.PrepareFrame(playable, info);
    }
    
    public override void OnGraphStop(Playable playable)
    {
        if (girl != null)
            girl.gameObject.GetComponent<BoyController>().ctrlState = BoyController.CtrlState.Player;
        base.OnGraphStop(playable);
    }
}
