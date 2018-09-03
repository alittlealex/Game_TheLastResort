using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour {
    public PlayableDirector mDirector;
    bool isChangeScene = false;
    public GameObject playGirl;

    void Start () {
        playGirl.GetComponent<BoyController>().enabled = true;
        playGirl.GetComponent<Controller>().enabled = false;
        playGirl.GetComponent<CapsuleCollider2D>().sharedMaterial = new PhysicsMaterial2D();
	}
	
	
	void Update () {
        if (mDirector.state == PlayState.Paused)
        {
            if (!isChangeScene)
            {
                Invoke("ChangeScene", 0.3f);
                isChangeScene = true;
            }
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("LevelForest1");
    }
}
