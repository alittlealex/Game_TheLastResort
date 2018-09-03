using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;


public class LevelBeginControll : MonoBehaviour {

    public PlayableDirector mDirector;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(mDirector.state == PlayState.Paused)
        {
            ChangeScene("LevelCave1");
        }
	}

    public void ChangeScene(string scnenName)
    {
        SceneManager.LoadScene(scnenName);
    }
}
