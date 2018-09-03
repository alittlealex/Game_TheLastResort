using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*路灯控制*/
public class LightContorl : MonoBehaviour {
    //序号
    public int num;
    private int switchState = 1;
    private int newSwitchState = 1;
    public GameObject boy;//小男孩
    public GameObject light_on;//开着的路灯的图片
    public GameObject light_off;//关着的路灯的图片
    public GameObject switch_on;//开着的开关的图片
    public GameObject switch_off;//关着的开关的图片
    public bool flag;//为true时开启，为false时关闭
    public GameObject Tools;
    public AudioSource soundEffect;
    public AudioClip switchAudio;


    void Start()
    {
        Tools = GameObject.Find("Tools");
        ////初始状态设定，路灯为开
        flag = true;
    }


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
            light_on.SetActive(flag);
            light_off.SetActive(!flag);
            switch_off.GetComponent<SpriteRenderer>().sortingOrder = 0;
            switch_on.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        //关闭
        else
        {
            flag = false;
            light_on.SetActive(flag);
            light_off.SetActive(!flag);
            switch_off.GetComponent<SpriteRenderer>().sortingOrder = 1;
            switch_on.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "PlayerBoy" && boy.GetComponent<Controller>().isDoingAttack == 1 && boy.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            Tools.GetComponent<Tools>().SendSwitchState(num, !flag);
        }
    }
}
