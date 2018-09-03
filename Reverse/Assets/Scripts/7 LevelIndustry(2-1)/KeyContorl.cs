using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyContorl : MonoBehaviour {
    public int KeyID;
    public GameObject boy;
    public GameObject girl;
    public GameObject key;
    public GameObject wall;
    public GameObject Tools;


	void Start () {
        Tools = GameObject.Find("Tools");
	}
	
	void Update () {
        boy.GetComponent<Controller>().isDoingAttack = -1;
        girl.GetComponent<Controller>().isDoingAttack = -1;

        if (Network.m_Actor.isKeyCheck)
        {
            if(Network.m_Actor.keyCheckAck.KeyID == KeyID)
            {
                wall.SetActive(false);
                key.SetActive(false);
                Network.m_Actor.isKeyCheck = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBoy" && boy.GetComponent<Controller>().isDoingAttack == 1)
        {
            Tools.GetComponent<Tools>().SendKeyCheckReq(KeyID);
        }
        if (collision.gameObject.tag == "PlayerGirl" && girl.GetComponent<Controller>().isDoingAttack == 1)
        {
            Tools.GetComponent<Tools>().SendKeyCheckReq(KeyID);
        }
        
    }
}
