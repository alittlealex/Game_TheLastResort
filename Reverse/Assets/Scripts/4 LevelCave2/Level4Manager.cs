using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Level4Manager : MonoBehaviour {

    public PlayableDirector mDirector;
    bool isChangeScene = false;
    public GameObject playGirl;
    public GameObject playBoy;
    private GameObject Tools;
    private AsyncOperation async;
    bool isLoadScene = false;
    bool isAsync = false;

    void Start()
    {
        playGirl.GetComponent<BoyController>().enabled = true;
        playGirl.GetComponent<Controller>().enabled = false;
        playGirl.GetComponent<CapsuleCollider2D>().sharedMaterial = new PhysicsMaterial2D();
        playBoy.GetComponent<BoyController>().enabled = true;
        playBoy.GetComponent<Controller>().enabled = false;
        playBoy.GetComponent<CapsuleCollider2D>().sharedMaterial = new PhysicsMaterial2D();
        Tools = GameObject.Find("Tools");
    }


    void Update()
    {
        if (mDirector.state == PlayState.Paused)
        {
            if (!isChangeScene)
            {
                Tools.GetComponent<Tools>().SendCheckTimeReq();
                isChangeScene = true;
            }
        }

        if (Network.m_Actor.isTimeOK && !isLoadScene)
        {
            isLoadScene = true;
            async = SceneManager.LoadSceneAsync("LevelForest2");
            async.allowSceneActivation = false;
        }

        if (async != null)
        {
            
            if (async.progress >= 0.89 && !isAsync)
            {
                isAsync = true;
                Network.m_Actor.isLoadSceneAsync = true;
            }
        }

        if (Network.m_Actor.isReceiveStartReq)
        {
            StartCoroutine("WaitToStart");
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(Network.m_Actor.startDelay - Network.totalDelay);
        Network.m_Actor.isReceiveStartReq = false;
        async.allowSceneActivation = true;
    }
    
}
