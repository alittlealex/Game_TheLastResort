using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LevelProcessManager : MonoBehaviour {

    public GameObject movie;

    private GameObject Tools;
    private AsyncOperation async;
    bool isLoadScene = false;
    bool isAsync = false;
    bool isChangeScene = false;

    void Start()
    {
        Tools = GameObject.Find("Tools");
    }


    void Update()
    {
        movie.GetComponent<VideoPlayer>().loopPointReached += EndReach;

        if (Network.m_Actor.isTimeOK && !isLoadScene)
        {
            isLoadScene = true;
            async = SceneManager.LoadSceneAsync("LevelGreenLand");
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

    void EndReach(UnityEngine.Video.VideoPlayer vp)
    {
        if (!isChangeScene)
        {
            Tools.GetComponent<Tools>().SendCheckTimeReq();
            isChangeScene = true;
        }
    }
}
