using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatInputField : MonoBehaviour {

    private InputField chatField;
    private GameObject Tools;

	void Start ()
    {
        chatField = GetComponent<InputField>();
        chatField.onEndEdit.AddListener(SendChatText);
        Tools = GameObject.Find("Tools");
	}
	
	void Update ()
    {
	}

    void SendChatText(string chatString)
    {
        Debug.Log(chatString);
        if (chatString != "")
        {
            Tools.GetComponent<Tools>().SendChatReq(chatString);
        }
        //清空输入框
        chatField.text = "";
    }
}
