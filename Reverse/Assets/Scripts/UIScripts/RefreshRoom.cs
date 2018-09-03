using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RefreshRoom : MonoBehaviour {
    //房间prefab
    public GameObject roomPrefab;
    //房间的父物体
    public GameObject content;
    //房间列表面板
    public GameObject RoomListPanel;
    //房间内面板
    public GameObject RoomPanel;
    //chat
    public GameObject textView;
    public GameObject chatContent;
    public ScrollRect scrollView;
    //房间列表
    [HideInInspector]
    public Dictionary<int,GameObject> roomList;

    private GameObject Tools;
	
	void Start ()
    {
        Tools = GameObject.Find("Tools");
	}
	
	
	void Update ()
    {
        //开始的时候刷出所有的房间信息
        if (Network.m_Actor.isGetRoomFinish)
        {
            foreach (var e in Network.m_Actor.getRoomAckList)
            {
                GameObject go =  Instantiate(roomPrefab,content.transform);
                go.transform.Find("RoomBG/RoomNum").GetComponent<Text>().text = e.RoomID.ToString();
                go.transform.Find("RoomBG/RoomPlayer").GetComponent<Text>().text = "Player: "+e.PlayerNum.ToString();
                go.transform.Find("RoomBG/JoinButton").GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Tools.GetComponent<Tools>().SendSelectRoomReq(e.RoomID);
                });
            }
            Network.m_Actor.getRoomAckList.Clear();
            Network.m_Actor.isGetRoomFinish = false;
        }
        //每有新房间被其他人建立，新建新房间
        if (Network.m_Actor.isNewRoom)
        {
            Debug.Log("newRoom");
            GameObject go = Instantiate(roomPrefab,content.transform);
            go.transform.Find("RoomBG/RoomNum").GetComponent<Text>().text = Network.m_Actor.newRoomAck.RoomID.ToString();
            go.transform.Find("RoomBG/RoomPlayer").GetComponent<Text>().text = "Player: " + Network.m_Actor.newRoomAck.PlayerNum.ToString();
            int roomID = Network.m_Actor.newRoomAck.RoomID;
            go.transform.Find("RoomBG/JoinButton").GetComponent<Button>().onClick.AddListener(delegate ()
            {
                Tools.GetComponent<Tools>().SendSelectRoomReq(roomID);
            });
            Network.m_Actor.isNewRoom = false;
        }
        //以房主身份创建房间
        if (Network.m_Actor.isInRoom && Network.m_Actor.firstInRoom)
        {
            Debug.Log("inRoom");
            RoomListPanel.SetActive(false);
            RoomPanel.SetActive(true);
            RoomPanel.transform.Find("Title").GetComponent<Text>().text = "Room" + Network.m_Actor.inRoomNum.ToString();
            if (Network.m_Actor.gender == 1)
                RoomPanel.transform.Find("Boy/BoyName").GetComponent<Text>().text = Network.m_Actor.name;
            else
                RoomPanel.transform.Find("Girl/GirlName").GetComponent<Text>().text = Network.m_Actor.name;
            //为房间里的Leave按钮添加事件
            RoomPanel.transform.Find("LeaveBtn").GetComponent<Button>().onClick.AddListener(delegate () 
            {
                Tools.GetComponent<Tools>().SendLeaveRoomReq(Network.m_Actor.inRoomNum);
            });
            RoomPanel.transform.Find("ReadyBtn").GetComponent<Button>().interactable = false;
            Network.m_Actor.firstInRoom = false;
        }
        //自己加别人房间或者别人加进自己房间
        if (Network.m_Actor.isInRoom && Network.m_Actor.secondInRoom)
        {
            RoomListPanel.SetActive(false);
            RoomPanel.SetActive(true);
            RoomPanel.transform.Find("Title").GetComponent<Text>().text = "Room" + Network.m_Actor.inRoomNum.ToString();
            //自己是房主
            if (Network.m_Actor.gender == 1)
            {
                RoomPanel.transform.Find("Boy/BoyName").GetComponent<Text>().text = Network.m_Actor.name;
                RoomPanel.transform.Find("Girl/GirlName").GetComponent<Text>().text = Network.m_Actor.anotherName;
                GameObject go = Instantiate(textView,chatContent.transform);
                go.GetComponent<Text>().text = "System: "+Network.m_Actor.anotherName+" has enter the room";
                StartCoroutine("ScrollBarBotton");
            }
            //自己加进别人的房间
            else
            {
                RoomPanel.transform.Find("Girl/GirlName").GetComponent<Text>().text = Network.m_Actor.name;
                RoomPanel.transform.Find("Boy/BoyName").GetComponent<Text>().text = Network.m_Actor.anotherName;
                //为房间里的Leave按钮添加事件
                RoomPanel.transform.Find("LeaveBtn").GetComponent<Button>().onClick.AddListener(delegate ()
                {
                    Tools.GetComponent<Tools>().SendLeaveRoomReq(Network.m_Actor.inRoomNum);
                });
                GameObject go = Instantiate(textView, chatContent.transform);
                go.GetComponent<Text>().text = "System: You have enter the room";
                StartCoroutine("ScrollBarBotton");
            }
            RoomPanel.transform.Find("ReadyBtn").GetComponent<Button>().interactable = true;
            Network.m_Actor.secondInRoom = false;
        }
        //如果房间列表刷新
        if (Network.m_Actor.isRefreshRoom)
        {
            Transform parentTransform = RoomListPanel.transform.Find("ScrollView/Viewport/Content").transform;
            foreach(Transform child in parentTransform)
            {
                //如果房间号匹配
                if(child.Find("RoomBG/RoomNum").gameObject.GetComponent<Text>().text == Network.m_Actor.refreshRoomAck.RoomID.ToString())
                {
                    if(Network.m_Actor.refreshRoomAck.PlayerNum == 0)
                    {
                        Destroy(child.gameObject);
                        break;
                    }
                    else
                    {
                        child.Find("RoomBG/RoomPlayer").gameObject.GetComponent<Text>().text = "Player: " + Network.m_Actor.refreshRoomAck.PlayerNum.ToString();
                        break;
                    }
                }
            }
            Network.m_Actor.isRefreshRoom = false;
        }
        //自己离开房间
        if (Network.m_Actor.isLeaveRoom)
        {
            RoomListPanel.SetActive(true);
            RoomPanel.SetActive(false);
            foreach(Transform child in chatContent.transform)
            {
                Destroy(child.gameObject);
            }
            RoomPanel.transform.Find("LeaveBtn").GetComponent<Button>().onClick.RemoveAllListeners();
            RoomPanel.transform.Find("Girl/GirlName").GetComponent<Text>().text = "";
            RoomPanel.transform.Find("Boy/BoyName").GetComponent<Text>().text = "";
            Network.m_Actor.anotherName = "";
            Network.m_Actor.inRoomNum = 0;
            Network.m_Actor.gender = 0;
            Network.m_Actor.isLeaveRoom = false;
        }
        //别人离开房间
        if (Network.m_Actor.isPlayerLeaveRoom)
        {
            RoomPanel.transform.Find("Boy/BoyName").GetComponent<Text>().text = Network.m_Actor.name;
            RoomPanel.transform.Find("Girl/GirlName").GetComponent<Text>().text = "";
            GameObject go = Instantiate(textView, chatContent.transform);
            go.GetComponent<Text>().text = "System: " + Network.m_Actor.anotherName + " has left the room";
            StartCoroutine("ScrollBarBotton");
            Network.m_Actor.anotherName = "";
            RoomPanel.transform.Find("ReadyBtn").GetComponent<Button>().interactable = false;
            Network.m_Actor.isPlayerLeaveRoom = false;
        }
	}

    public void OnClickNewRoom()
    {
        Tools.GetComponent<Tools>().SendCreateRoomReq();
    }
   
    public void OnClickReady()
    {
        if(Network.m_Actor.anotherName != "")
        {
            Tools.GetComponent<Tools>().SendCheckTimeReq();
            RoomPanel.transform.Find("LeaveBtn").GetComponent<Button>().interactable = false;
        }
    }

    public void OnClickReturnButton()
    {
        Transform parentTransform = RoomListPanel.transform.Find("ScrollView/Viewport/Content").transform;
        foreach (Transform child in parentTransform)
        {
            Destroy(child.gameObject);
        }
        Tools.GetComponent<Tools>().SendReturnButton();
    }

    IEnumerator ScrollBarBotton()
    {
        yield return new WaitForEndOfFrame();
        scrollView.verticalNormalizedPosition = 0f;
    }
}
