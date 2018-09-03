using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelXManager : MonoBehaviour {

    public PlayableDirector mDirector;
    bool isChangeScene = false;
    public GameObject Tools;

    // Use this for initialization
    void Start () {
        Tools = GameObject.Find("Tools");
	}
	
	// Update is called once per frame
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
        Tools.GetComponent<Tools>().SendGameisOverReq();
        SceneManager.LoadScene("LevelMenu");
    }
}
