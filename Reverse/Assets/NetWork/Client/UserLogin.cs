//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;




//public class UserLogin : MonoBehaviour {
//    [SerializeField]
//    InputField m_UserName;
//    bool isOnClick;

//	// Use this for initialization
//	void Start () {
//        isOnClick = false;
//        Application.runInBackground = true;
//        DontDestroyOnLoad(gameObject);
//    }

//    // DataStructure DS = new DataStructure();

//    public void OnClickLogin()
//    {
//        var userName = m_UserName.text;
//        //TODO 检查userName 合法
//        var loginMessage = new mmopb.login_req();
//        loginMessage.name = userName;
//        isOnClick = true;
//        ClientTest.m_Actor.SendMessage(loginMessage);

//    }
//	// Update is called once per frame
//	void Update () {

//        if (isOnClick == false)
//        {
//            return;
//        }

//        var StateMessage_req = new mmopb.StateStruct_req();

//        StateMessage_req.name = m_UserName.text;
//        StateMessage_req.life = 1;
//        StateMessage_req.direction = 1;
//        StateMessage_req.state = 1;

//        ClientTest.m_Actor.SendMessage(StateMessage_req);

//	}
//}
