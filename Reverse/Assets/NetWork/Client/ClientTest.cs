//using Net.Tcp;
//using ProtoBuf;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Net.Sockets;
//using UnityEngine;

//public class ClientTest : MonoBehaviour {
    
//    public static CActor m_Actor;
//    private Net.Tcp.TcpClient m_Client;
//    // Use this for initialization
//    void Start () {
//        m_Actor = new CActor();
//        m_Client = new Net.Tcp.TcpClient(m_Actor);
//        m_Client.Start();
//        DontDestroyOnLoad(gameObject);
//        Application.runInBackground = true;
//    }

//    void OnDestroy()
//    {
//        //m_Client.Close();
//    }
//    // Update is called once per frame
//    void Update () {
//        m_Client.Update();
//    }
//}

//public class CActor : ICTcpActor
//{
//   // public GameObject cube;

//    public void SetConnName(string connName)
//    {

//    }
//    public void Handle(byte[] message)
//    {
//        string msgName;
//        var msg = ProtoHelper.DecodeWithName(message, out msgName);
//        FileStream fs = new FileStream("C:\\ClientTest\\Test1.txt", FileMode.OpenOrCreate);
//        StreamWriter sw = new StreamWriter(fs);

//        Debug.Log("CActor->" + msgName);

//        if(msg is mmopb.login_ack)
//        {
//            var ack = msg as mmopb.login_ack;
//            if(ack.succ)
//            {
//                //UnityEngine.SceneManagement.SceneManager.LoadScene("Logined");   
//                Debug.Log("Login succ");
//            }
//            sw.Write("login_ack\n");

//        }
//        else if (msg is mmopb.StateStruct_ack)
//        {
//            var ack = msg as mmopb.StateStruct_ack;
//            if (ack.succ)
//            {
//                Debug.Log("CActorSSACK->" + msgName);
//            }
//            sw.Write("ss_ack\n");
//        }
//        else if (msg is mmopb.StateStruct_req)
//        {
//            sw.Write("StateStruct_req\n");
//        }
//        else
//        {
//            sw.Write("other type\n");
//        }
//        sw.Flush();
//        sw.Close();
//        fs.Close();
//    }

//    ICTcpConnection tcpConnection;
//    public void Initialize(ICTcpConnection conn)
//    {
//        tcpConnection = conn;
//        tcpConnection.Connect("172.20.120.136", 26001);
//    }

//    public void OnConnected(SocketError err)
//    {
//        Debug.Log(err);
//    }

//    public void SendMessage(object o)
//    {
//        tcpConnection.Send(ProtoHelper.EncodeWithName(o));

//    }
//    public void OnDisconnected(SocketError err)
//    {

//    }
//}
