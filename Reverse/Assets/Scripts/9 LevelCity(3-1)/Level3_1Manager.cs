using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3_1Manager : MonoBehaviour {

    private GameObject Tools;
    bool isBoyArrive = false;
    bool isGirlArrive = false;
    bool isChangeScene = false;
    private AsyncOperation async;
    bool isLoadScene = false;
    bool isAsync = false;

    // Use this for initialization
    void Start () {
        Tools = GameObject.Find("Tools");
    }
	
	// Update is called once per frame
	void Update () {
        if (isBoyArrive && isGirlArrive && !isChangeScene)
        {
            Tools.GetComponent<Tools>().SendCheckTimeReq();
            isChangeScene = true;
        }

        if (Network.m_Actor.isTimeOK && !isLoadScene)
        {
            isLoadScene = true;
            async = SceneManager.LoadSceneAsync("LevelProcess");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBoy")
            isBoyArrive = true;
        if (collision.gameObject.tag == "PlayerGirl")
            isGirlArrive = true;
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(Network.m_Actor.startDelay - Network.totalDelay);
        Network.m_Actor.isReceiveStartReq = false;
        async.allowSceneActivation = true;
    }
}
