using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class PortalTriger : MonoBehaviour {
    bool playerBoyIn = false;
    bool playerGirlIn = false;
    bool isChangeScene = false;
    public GameObject playerBoy;
    public GameObject playerGirl;
    public GameObject boyImage;
    public GameObject girlImage;
    public PlayableDirector mDirector;
    private GameObject Tools;
    private AsyncOperation async;
    bool isLoadScene = false;
    bool isAsync = false;

    void Start () {
        Tools = GameObject.Find("Tools");
    }

	void Update ()
    {
	    if(playerBoyIn && playerGirlIn && !isChangeScene)
        {
            playerGirl.SetActive(false);
            playerBoy.SetActive(false);
            boyImage.SetActive(true);
            girlImage.SetActive(true);
            mDirector.Play();
            StartCoroutine("ChangeScene");
            isChangeScene = true;
        }

        if (Network.m_Actor.isTimeOK && !isLoadScene)
        {
            async = SceneManager.LoadSceneAsync("LevelBack");
            async.allowSceneActivation = false;
            isLoadScene = true;
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
            Network.m_Actor.isReceiveStartReq = false;
            StartCoroutine("WaitToStart");
            async.allowSceneActivation = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBoy")
        {
            playerBoyIn = true;
        }
        if(collision.gameObject.tag == "PlayerGirl")
        {
            playerGirlIn = true;
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(10);
        Tools.GetComponent<Tools>().SendCheckTimeReq();
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(Network.m_Actor.startDelay - Network.totalDelay);
    }
}
