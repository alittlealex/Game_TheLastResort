using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Level3Manager : MonoBehaviour {
    public PlayableDirector mDirector;
    bool isChangeScene = false;
    public GameObject playerGirl;

    void Start ()
    {
        playerGirl.GetComponent<Controller>().enabled = false;
        playerGirl.GetComponent<BoyController>().enabled = true;
        playerGirl.GetComponent<CapsuleCollider2D>().sharedMaterial = new PhysicsMaterial2D();
    }

    void Update()
    {
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
        SceneManager.LoadScene("LevelCave2");
    }
}
