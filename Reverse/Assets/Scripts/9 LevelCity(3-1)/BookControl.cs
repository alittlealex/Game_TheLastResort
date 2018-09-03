using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BookControl : MonoBehaviour {

    public GameObject book;//本体书作为开关
    public GameObject TalkBlock;//对话框
    public GameObject robort;//机器人
    public GameObject boy;
    public GameObject girl;
    //是否已经被点击过，得到了密码
    private bool isPassword;
    [HideInInspector]
    public int randnum;
    string s = "47";

    public GameObject Tools;
	
	void Start ()
    {
        isPassword = false;
        randnum = 47;
        Tools = GameObject.Find("Tools");
    }
	
	
	void Update ()
    {
        if (Network.m_Actor.isReceiveBookState && !isPassword)
        {
            Debug.Log("book on");
            TalkBlock.SetActive(true);
            TalkBlock.GetComponentInChildren<Text>().text = s;
            robort.GetComponent<RobortControl>().on = true;
            Network.m_Actor.isReceiveBookState = false;
            isPassword = true;
        }
    }

    private void FixedUpdate()
    {
        if(boy.GetComponent<Controller>().isDoingAttack == 1 && boy.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            if(System.Math.Abs(boy.transform.position.x - book.transform.position.x) <= 0.5 && System.Math.Abs(boy.transform.position.y - book.transform.position.y) <= 2)
            {
                Tools.GetComponent<Tools>().SendBookChange();
            }
        }

        if(girl.GetComponent<Controller>().isDoingAttack == 1 && girl.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            if(System.Math.Abs(girl.transform.position.x - book.transform.position.x) <= 0.5 && System.Math.Abs(girl.transform.position.y - book.transform.position.y) <= 2)
            {
                Tools.GetComponent<Tools>().SendBookChange();
            }
        }
    }
}
