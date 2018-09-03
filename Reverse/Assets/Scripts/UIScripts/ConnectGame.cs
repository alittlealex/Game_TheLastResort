using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ConnectGame : MonoBehaviour {
    public GameObject Tools;
    public GameObject nameField;
    private AsyncOperation async;
    bool isLoadScene = false;
    bool isAsync = false;
    bool isClickOK = false;
    bool isNameSent = false;

	void Start () {
	}
	
	void Update () {
        if (Network.m_Actor.isTimeOK && !isLoadScene)
        {
            isLoadScene = true;
            async = SceneManager.LoadSceneAsync("LevelBegin");
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

    /// <summary>
    /// 点击OK触发事件
    /// </summary>
    public void OnClickOK()
    {
        string name = nameField.GetComponent<Text>().text;
        if (Network.m_Actor.isConnected && name != "")
        {
            Network.m_Actor.name = name;
            Tools.GetComponent<Tools>().SendGetRoomReq(name);
            isNameSent = true;
        }
    }

    IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(Network.m_Actor.startDelay - Network.totalDelay);
        Network.m_Actor.isReceiveStartReq = false;
        async.allowSceneActivation = true;
    }
}
