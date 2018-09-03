using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 发送加入服务器信息
    /// </summary>
    public void SendJoinGameReq()
    {
        var startGame = new mmopb.JoinGame_req();
        Network.m_Actor.SendMessage(startGame);
    }

    /// <summary>
    /// 发送得到房间列表的请求
    /// </summary>
    /// <param name="name"></param>
    public void SendGetRoomReq(string name)
    {
        mmopb.GetRoom_req req = new mmopb.GetRoom_req();
        req.name = name;
        Network.m_Actor.SendMessage(req);
    }

    public void SendCreateRoomReq()
    {
        mmopb.CreateRoom_req req = new mmopb.CreateRoom_req();
        Network.m_Actor.SendMessage(req);
    }

    public void SendSelectRoomReq(int roomID)
    {
        mmopb.SelectRoom_req req = new mmopb.SelectRoom_req();
        req.RoomID = roomID;
        Network.m_Actor.SendMessage(req);
    }

    public void SendLeaveRoomReq(int roomID)
    {
        mmopb.LeaveRoom_req req = new mmopb.LeaveRoom_req();
        req.RoomID = roomID;
        Network.m_Actor.SendMessage(req);
    }

    public void SendReturnButton()
    {
        mmopb.ReturnButton_req req = new mmopb.ReturnButton_req();
        Network.m_Actor.SendMessage(req);
    }

    /// <summary>
    /// 每隔0.01s发送一次
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitTimeCheckReq(string name)
    {
        //Debug.Log(Network.m_Actor);
        var checkTime = new mmopb.CheckTime_req();
        yield return new WaitForSeconds(0.01f);
        checkTime.LocalTime = Time.time;
        checkTime.RoomID = Network.m_Actor.inRoomNum;
        //checkTime.name = name;
        Network.m_Actor.SendMessage(checkTime);
    }

    IEnumerator WaitTimeCheckReqWithoutName()
    {
        //Debug.Log(Network.m_Actor);
        var checkTime = new mmopb.CheckTime_req();
        yield return new WaitForSeconds(0.01f);
        checkTime.LocalTime = Time.time;
        checkTime.RoomID = Network.m_Actor.inRoomNum;
        Network.m_Actor.SendMessage(checkTime);
    }

    /// <summary>
    /// 发送时延计算请求
    /// </summary>
    public void SendCheckTimeReq(string name)
    {
        for (int i = 0; i < 7; i++)
        {
            StartCoroutine("WaitTimeCheckReq",name);
        }
    }

    public void SendCheckTimeReq()
    {
        //Debug.Log("change to level5");
        for (int i = 0; i < 7; i++)
        {
            StartCoroutine("WaitTimeCheckReqWithoutName");
        }
    }

    /// <summary>
    /// 发送时延计算确认
    /// </summary>
    /// <param name="m_Actor"></param>
    public void SendCheckTimeOK()
    {
        var ackTime = new mmopb.TimeIsOK_req();
        ackTime.RoomID = Network.m_Actor.inRoomNum;
        Network.m_Actor.SendMessage(ackTime);
    }

    /// <summary>
    /// 计算平均时延
    /// </summary>
    /// <param name="list"></param>
    /// <returns>平均时延</returns>
    public float CountDelayTime(List<float> list)
    {
        float DelayTime = 0;

        FindBiggest(ref list);
        FindSmallest(ref list);

        foreach (var delay in list)
        {
            DelayTime += delay;
        }
        return DelayTime / list.Count;
    }

    /// <summary>
    /// 去掉最大时延
    /// </summary>
    /// <param name="list"></param>
    private void FindBiggest(ref List<float> list)
    {
        float big = list[0];
        int num = 0;
        for (int i = 1; i < list.Count; i++)
        {
            if (big < list[i])
            {
                big = list[i];
                num = i;
            }
        }
        list.Remove(list[num]);
    }

    /// <summary>
    /// 去掉最小时延
    /// </summary>
    /// <param name="list"></param>
    public void FindSmallest(ref List<float> list)
    {
        float small = list[0];
        int num = 0;
        for (int i = 1; i < list.Count; i++)
        {
            if (small > list[i])
            {
                small = list[i];
                num = i;
            }
        }
        list.Remove(list[num]);
    }

    public void SendUserActorReq(mmopb.UserActor_req req)
    {
        Network.m_Actor.SendMessage(req);
    }

    public void SendCalculatorResultChangeReq(int res, int num, CaculatorResult result)
    {
        mmopb.CalculatorResultChange_req req = new mmopb.CalculatorResultChange_req();
        req.cal_1 = result.results[0];
        req.cal_2 = result.results[1];
        req.cal_3 = result.results[2];
        req.cal_4 = result.results[3];
        req.cal_5 = result.results[4];
        req.cal_6 = result.results[5];
        switch (num)
        {
            case 1:
                req.cal_1 = res;
                break;
            case 2:
                req.cal_2 = res;
                break;
            case 3:
                req.cal_3 = res;
                break;
            case 4:
                req.cal_4 = res;
                break;
            case 5:
                req.cal_5 = res;
                break;
            case 6:
                req.cal_6 = res;
                break;
        }
        req.RoomID = Network.m_Actor.inRoomNum;
        Network.m_Actor.SendMessage(req);
    }

    public void SendSwitchState(int num, bool flag)
    {
        mmopb.SwitchStateChange_req req = new mmopb.SwitchStateChange_req();
        req.id = num;
        if (flag)
        {
            req.state = 1;
        }
        else
        {
            req.state = -1;
        }
        req.RoomID = Network.m_Actor.inRoomNum; 
        Network.m_Actor.SendMessage(req);
    }

    public void SendSetPosition(Vector2 position)
    {
        mmopb.SetPosition_req req = new mmopb.SetPosition_req();
        req.gender = Network.m_Actor.gender;
        req.position = new mmopb.Vector2();
        req.position.x = position.x;
        req.position.y = position.y;
        req.RoomID = Network.m_Actor.inRoomNum;
        Network.m_Actor.SendMessage(req);
    }

    public void SendBookChange()
    {
        mmopb.BookStateChange_req req = new mmopb.BookStateChange_req();
        req.state = true;
        req.RoomID = Network.m_Actor.inRoomNum; 
        Network.m_Actor.SendMessage(req);
    }

    public void SendUmberllaReq(int _gender)
    {
        mmopb.UmberllaCheck_req req = new mmopb.UmberllaCheck_req();
        req.RoomID = Network.m_Actor.inRoomNum;
        req.gender = _gender;
        Network.m_Actor.SendMessage(req);
    }

    public void SendMaskCheckReq(int _gender, Vector2 _position)
    {
        mmopb.MaskCheck_req req = new mmopb.MaskCheck_req();
        req.gender = _gender;
        req.RoomID = Network.m_Actor.inRoomNum;
        req.state = true;
        req.position = new mmopb.Vector2();
        req.position.x = _position.x;
        req.position.y = _position.y;
        Network.m_Actor.SendMessage(req);
    }

    public void SendKeyCheckReq(int keyID)
    {
        mmopb.KeyCheck_req req = new mmopb.KeyCheck_req();
        req.RoomID = Network.m_Actor.inRoomNum;
        req.KeyID = keyID;
        req.state = true;
        Network.m_Actor.SendMessage(req);
    }

    public void SendGameisOverReq()
    {
        mmopb.GameIsOver_req req = new mmopb.GameIsOver_req();
        req.RoomID = Network.m_Actor.inRoomNum;
        Network.m_Actor.SendMessage(req);
    }

    public void SendChatReq(string chatContent)
    {
        mmopb.Chat_req req = new mmopb.Chat_req();
        req.chat = chatContent;
        req.gender = Network.m_Actor.gender;
        req.RoomID = Network.m_Actor.inRoomNum;
        Network.m_Actor.SendMessage(req);
    }
}
