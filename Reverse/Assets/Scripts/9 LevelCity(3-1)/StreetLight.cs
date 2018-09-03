using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StreetLight : MonoBehaviour {
    //序号
    public int num;
    private int switchState = 1;
    private int newSwitchState = 1;
    public GameObject boy;
    public GameObject girl;
    public GameObject light_on;
    public GameObject light_off;
    public GameObject switch_on;
    public GameObject switch_off;
    public bool flag;//为true时开启，为false时关闭
    public GameObject Tools;
    public AudioSource soundEffect;
    public AudioClip switchAudio;

    void Start () {
        Tools = GameObject.Find("Tools");
        light_on.SetActive(true);
        light_off.SetActive(false);
        switch_on.SetActive(true);
        switch_off.SetActive(false);
        flag = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Network.m_Actor.isNewSwitch)
        {
            if (num == Network.m_Actor.switchID)
            {
                newSwitchState = Network.m_Actor.switchState;
                Network.m_Actor.isNewSwitch = false;
            }
        }
        if(newSwitchState != switchState)
        {
            Debug.Log("playeffects");
            switchState = newSwitchState;
            soundEffect.clip = switchAudio;
            soundEffect.Play();
        }
        //开启
        if(switchState == 1)
        {
            flag = true;
            light_on.SetActive(flag);
            light_off.SetActive(!flag);
            switch_off.SetActive(!flag);
            switch_on.SetActive(flag);
        }
        //关闭
        else
        {
            flag = false;
            light_on.SetActive(flag);
            light_off.SetActive(!flag);
            switch_off.SetActive(!flag);
            switch_on.SetActive(flag);
        }

        if (flag)
        {
            if (System.Math.Abs(boy.transform.position.x - light_on.transform.position.x) <= 0.5f && boy.GetComponent<Controller>().gender == Network.m_Actor.gender)
            {
                boy.GetComponent<Controller>().dead = true;
            }
        }
        //girl.GetComponent<Controller>().isDoingAttack = -1;
        //boy.GetComponent<Controller>().isDoingAttack = -1;
    }


    private void FixedUpdate()
    {
        if (girl.GetComponent<Controller>().isDoingAttack == 1 && girl.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            if (System.Math.Abs(girl.transform.position.x - switch_on.transform.position.x) <= 0.5f)
            {
                Debug.Log("GirlLight");
                Tools.GetComponent<Tools>().SendSwitchState(num, !flag);
            }
        }
    }

    private void LateUpdate()
    {
        
    }
}
