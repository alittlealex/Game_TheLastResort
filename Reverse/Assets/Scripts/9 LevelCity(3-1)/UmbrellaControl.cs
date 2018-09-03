using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UmbrellaControl : MonoBehaviour
{

    public GameObject umbrella;
    public GameObject boy;
    public GameObject girl;
    public int owner;//0表示没有主人，1表示主人是男孩，2表示主人是女孩
    public GameObject wall;
    public GameObject Tools;
    private void Awake()
    {
        owner = 0;
    }
    void Start()
    {
        Tools = GameObject.Find("Tools");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Network.m_Actor.isReceiveUmberlla)
        {
            if(Network.m_Actor.umberllaAck.gender == 1)
            {
                owner = 1;
            }
            else
            {
                owner = 2;
            }
            Network.m_Actor.isReceiveUmberlla = false;
        }
        
        if (owner == 1)
        {
            umbrella.transform.position = new Vector3(boy.transform.position.x, boy.transform.position.y + 0.8f, 0);
            if (System.Math.Abs(girl.transform.position.x - boy.transform.position.x) >= 1.5&&wall.activeSelf)
            {
                girl.GetComponent<Controller>().dead = true;
            }
        }
        if (owner == 2)
        {
            umbrella.transform.position = new Vector3(girl.transform.position.x, girl.transform.position.y + 0.8f, 0);
            if (System.Math.Abs(girl.transform.position.x - boy.transform.position.x) >= 1.5 && wall.activeSelf)
            {
                boy.GetComponent<Controller>().dead = true;
            }
        }
        if(owner == 0)
        {
            if (System.Math.Abs(girl.transform.position.x - umbrella.transform.position.x) >= 5 && wall.activeSelf)
            {
                girl.GetComponent<Controller>().dead = true;
            }
            if (System.Math.Abs(girl.transform.position.x - umbrella.transform.position.x) >= 5 && wall.activeSelf)
            {
                boy.GetComponent<Controller>().dead = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (girl.GetComponent<Controller>().isDoingAttack == 1 && girl.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            if (System.Math.Abs(girl.transform.position.x - umbrella.transform.position.x) <= 0.8)
            {
                Tools.GetComponent<Tools>().SendUmberllaReq(girl.GetComponent<Controller>().gender);
            }
            //
        }

        if (boy.GetComponent<Controller>().isDoingAttack == 1 && boy.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            if (System.Math.Abs(boy.transform.position.x - umbrella.transform.position.x) <= 0.8)
            {
                Tools.GetComponent<Tools>().SendUmberllaReq(boy.GetComponent<Controller>().gender);
            }
            //
        }
        boy.GetComponent<Controller>().isDoingAttack = -1;
        girl.GetComponent<Controller>().isDoingAttack = -1;
    }

    
}
