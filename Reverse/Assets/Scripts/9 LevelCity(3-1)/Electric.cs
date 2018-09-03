using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Electric : MonoBehaviour {
    public int num;
    private int switchState = 1;
    private int newSwitchState = 1;
    public GameObject boy;
    public GameObject girl;
    public GameObject electric;
    public GameObject switch_on;
    public GameObject switch_off;
    public bool flag;//为true时开启，为false时关闭
    public GameObject Tools;
    public AudioSource soundEffect;
    public AudioClip switchAudio;

    void Start()
    {
        Tools = GameObject.Find("Tools");
        electric.SetActive(true);
        switch_on.SetActive(true);
        switch_off.SetActive(false);
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Network.m_Actor.isNewSwitch)
        {
            if (num == Network.m_Actor.switchID)
            {
                newSwitchState = Network.m_Actor.switchState;
                Network.m_Actor.isNewSwitch = false;
            }
        }
        if (newSwitchState != switchState)
        {
            switchState = newSwitchState;
            soundEffect.clip = switchAudio;
            soundEffect.Play();
        }
        //开启
        if (switchState == 1)
        {
            flag = true;
            electric.SetActive(flag);
            switch_off.SetActive(!flag);
            switch_on.SetActive(flag);
        }
        //关闭
        else
        {
            flag = false;
            electric.SetActive(flag);
            switch_off.SetActive(!flag);
            switch_on.SetActive(flag);
        }
        
    }

    private void FixedUpdate()
    {
        if (boy.GetComponent<Controller>().isDoingAttack == 1 && boy.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            //Debug.LogError("boyElec");
            if (System.Math.Abs(boy.transform.position.x - switch_on.transform.position.x) <= 0.5f)
            {
                
                Tools.GetComponent<Tools>().SendSwitchState(num, !flag);
                //boy.GetComponent<Controller>().isDoingAttack = -1;
            }
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerGirl" && flag && girl.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            girl.GetComponent<Controller>().dead = true;
            //Invoke("Restart", 2);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene("Level3-1");
    }
}
