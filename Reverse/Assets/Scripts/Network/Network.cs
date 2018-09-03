using Net.Tcp;
using ProtoBuf;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Network : MonoBehaviour
{
    public static float totalDelay;
    public GameObject Tools;
    public static CActor m_Actor;
    private Net.Tcp.TcpClient m_Client;

    void Start()
    {
        m_Actor = new CActor();
        m_Client = new Net.Tcp.TcpClient(m_Actor);
        m_Client.Start();
        Tools.GetComponent<Tools>().SendJoinGameReq();
        DontDestroyOnLoad(gameObject);
    }

    void OnDestroy()
    {
        m_Client.Close();
    }
    
    void Update()
    {
        m_Client.Update();
        if(m_Actor.DelayTime.Count == 7)
        {
            totalDelay = Tools.GetComponent<Tools>().CountDelayTime(m_Actor.DelayTime);
            m_Actor.isTimeOK = true;
            //text.GetComponent<Text>().text += "first";
            Debug.Log(totalDelay);
            m_Actor.DelayTime.Clear();
        }
        Debug.Log(m_Actor.isTimeOK +" "+ m_Actor.isLoadSceneAsync);
        if (m_Actor.isTimeOK && m_Actor.isLoadSceneAsync)
        {
            Tools.GetComponent<Tools>().SendCheckTimeOK();
            m_Actor.isTimeOK = false;
            m_Actor.isLoadSceneAsync = false;
        }
    }
}



public class CActor : ICTcpActor
{
    public List<mmopb.GetRoom_ack> getRoomAckList = new List<mmopb.GetRoom_ack>() ;
    public bool isGetRoomFinish = false;
    public mmopb.NewRoom_ack newRoomAck;
    //有新房间创建
    public bool isNewRoom = false;
    //第一次进房
    public bool firstInRoom = false;
    public bool secondInRoom = false;
    //进房间的号码
    public int inRoomNum;
    //是否在房间里
    public bool isInRoom = false;
    //收到的刷新房间消息
    public mmopb.RefreshRoomInfo_ack refreshRoomAck;
    //是否需要刷新房间信息
    public bool isRefreshRoom = false;
    //自己是不是离开了房间
    public bool isLeaveRoom = false;
    //有人离开房间接到的ack
    public mmopb.PlayerLeaveRoom_ack playerLeaveRoomAck;
    //是否有人离开自己的房间
    public bool isPlayerLeaveRoom = false;
    //聊天消息
    public mmopb.Chat_ack chatAck;
    //是否有新的聊天消息
    public bool isChatAck = false;
    //延迟时间测试列表
    public List<float> DelayTime = new List<float>();
    //开始的延迟
    public float startDelay;
    public bool isConnected = false;
    public bool isTimeOK = false;
    public bool isLoadSceneAsync = false;
    public bool isReceiveStartReq = false;
    public bool isReceiveActorAck = false;
    public string name;
    public string anotherName;
    public int gender;
    public List<mmopb.UserActor_ack> actorAckList = new List<mmopb.UserActor_ack>();
    public mmopb.CalculatorResultChange_ack calculatorResultChange_Ack;
    public bool isNewResult = false;
    public int switchState;
    public int switchID;
    public bool isNewSwitch = false;
    public Vector2 newSetPosition;
    public bool isReceiveSetPosition = false;
    public bool isReceiveBookState = false;
    public mmopb.UmberllaCheck_ack umberllaAck;
    public bool isReceiveUmberlla = false;
    public mmopb.MaskCheck_ack maskCheckAck;
    public bool isReceiveMaskCheck = false;
    public mmopb.KeyCheck_ack keyCheckAck;
    public bool isKeyCheck = false;

    public void SetConnName(string connName)
    {

    }
    public void Handle(byte[] message)
    {
        string msgName;
        var msg = ProtoHelper.DecodeWithName(message, out msgName);
        //连接上服务器
        if(msg is mmopb.JoinGame_ack)
        {
            isConnected = true;
            var joinGameAck = msg as mmopb.JoinGame_ack;
        }
        //第一次加入房间列表获取所有房间
        else if(msg is mmopb.GetRoom_ack)
        {
            var getRoomAck = msg as mmopb.GetRoom_ack;
            getRoomAckList.Add(getRoomAck);
        }
        //获取所有房间结束
        else if(msg is mmopb.GetRoom_fin)
        {
            isGetRoomFinish = true;
        }
        //每有新房间被其他人建立，新建新房间
        else if (msg is mmopb.NewRoom_ack)
        {
            Debug.Log("NewRoom_ack");
            newRoomAck = msg as mmopb.NewRoom_ack;
            isNewRoom = true;
        }
        //以房主身份创建房间
        else if (msg is mmopb.CreateRoom_ack)
        {
            Debug.Log("CreateRoom_ack");
            var createRoomAck = msg as mmopb.CreateRoom_ack;
            gender = createRoomAck.gender;
            inRoomNum = createRoomAck.RoomID;
            firstInRoom = true;
            isInRoom = true;
        }
        //加入房间失败
        else if(msg is mmopb.SelectRoom_err)
        {
            Debug.Log("加入房间失败");
        }
        //加入房间
        else if(msg is mmopb.SelectRoom_ack)
        {
            var selectRoonAck = msg as mmopb.SelectRoom_ack;
            inRoomNum = selectRoonAck.RoomID;
            gender = selectRoonAck.gender;
            anotherName = selectRoonAck.name;
            isInRoom = true;
            secondInRoom = true;
        }
        //刷新房间列表
        else if(msg is mmopb.RefreshRoomInfo_ack)
        {
            refreshRoomAck = msg as mmopb.RefreshRoomInfo_ack;
            isRefreshRoom = true;
        }
        //自己退房
        else if(msg is mmopb.LeaveRoom_ack)
        {
            isLeaveRoom = true;
        }
        //别人退房
        else if(msg is mmopb.PlayerLeaveRoom_ack)
        {
            playerLeaveRoomAck = msg as mmopb.PlayerLeaveRoom_ack;
            gender = playerLeaveRoomAck.gender;
            isPlayerLeaveRoom = true;
        }
        //别人加进来房间
        else if(msg is mmopb.NewPlayerJoin_ack)
        {
            Debug.Log("newPlayjoin");
            var newPlayerJoinAck = msg as mmopb.NewPlayerJoin_ack;
            anotherName = newPlayerJoinAck.name;
            secondInRoom = true;
        }
        //聊天ack
        else if(msg is mmopb.Chat_ack)
        {
            chatAck = msg as mmopb.Chat_ack;
            isChatAck = true;
        }
        //返回主菜单
        else if(msg is mmopb.ReturnToHomePage_ack)
        {
            SceneManager.LoadScene("LevelMenu");
        }
        //计算时延
        else if(msg is mmopb.CheckTime_ack)
        {
            Debug.Log(msgName);
            var ackmsg = msg as mmopb.CheckTime_ack;
            float delay = Time.time - ackmsg.LocalTime;
            DelayTime.Add(delay);
        }
        //开始游戏
        else if(msg is mmopb.StartGame_req)
        {
            var startGameReq = msg as mmopb.StartGame_req;
            startDelay = startGameReq.StartDelay;
            Debug.Log("startDelay:" + startDelay);
            isReceiveStartReq = true;
        }
        //角色控制
        else if(msg is mmopb.UserActor_ack)
        {
            var userActorAck = msg as mmopb.UserActor_ack;
            actorAckList.Add(userActorAck);
            isReceiveActorAck = true;
        }
        //道具:计算器
        else if(msg is mmopb.CalculatorResultChange_ack)
        {
            var calculatorResultAck = msg as mmopb.CalculatorResultChange_ack;
            calculatorResultChange_Ack = calculatorResultAck;
            isNewResult = true;
        }
        //道具：开关
        else if(msg is mmopb.SwitchStateChange_ack)
        {
            switchState = (msg as mmopb.SwitchStateChange_ack).state;
            switchID = (msg as mmopb.SwitchStateChange_ack).id;
            isNewSwitch = true;
        }
        //强制同步位置
        else if(msg is mmopb.SetPosition_ack)
        {
            var setPosition = msg as mmopb.SetPosition_ack;
            if(setPosition.gender != gender)
            {
                newSetPosition = new Vector2(setPosition.position.x, setPosition.position.y);
                isReceiveSetPosition = true;
            }
        }
        //道具：书本
        else if(msg is mmopb.BookStateChange_ack)
        {
            var bookStateAck = msg as mmopb.BookStateChange_ack;
            if (bookStateAck.state)
            {
                isReceiveBookState = true;
            }
        }
        //道具：伞
        else if(msg is mmopb.UmberllaCheck_ack)
        {
            umberllaAck = msg as mmopb.UmberllaCheck_ack;
            isReceiveUmberlla = true;
        }
        //道具：面具
        else if(msg is mmopb.MaskCheck_ack)
        {
            maskCheckAck = msg as mmopb.MaskCheck_ack;
            isReceiveMaskCheck = true;
        }
        //道具：钥匙
        else if(msg is mmopb.KeyCheck_ack)
        {
            keyCheckAck = msg as mmopb.KeyCheck_ack;
            isKeyCheck = true;
        }
    }

    ICTcpConnection tcpConnection;
    public void Initialize(ICTcpConnection conn)
    {
        tcpConnection = conn;
        //136
        tcpConnection.Connect("172.20.120.136", 26001);
    }

    public void OnConnected(SocketError err)
    {
        Debug.Log(err);
    }

    public void SendMessage(object o)
    {
        tcpConnection.Send(ProtoHelper.EncodeWithName(o));

    }
    public void OnDisconnected(SocketError err)
    {

    }
    
}

