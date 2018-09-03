using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorContorl : MonoBehaviour {
    public GameObject Tools;
    public int num;
    private int switchState = 1;
    private int newSwitchState = 1;
    public GameObject elevator;
    public GameObject boy;
    public GameObject girl;
    public bool stage;//true表示一楼，false表示二楼
    private readonly float down = 145.91f;
    private readonly float up = 150.38f;
    public AudioSource soundEffects;
    public AudioClip elevatorAudio;

    void Start ()
    {
        Tools = GameObject.Find("Tools");
        stage = true;
    }
	
	void Update ()
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
            Debug.Log("playeffects");
            switchState = newSwitchState;
            soundEffects.clip = elevatorAudio;
            soundEffects.Play();
        }
        
        if(switchState == 1)
        {
            stage = true;
        }
        else
        {
            stage = false;
        }

        //去往一楼
        if (stage && elevator.transform.position.y < up)
        {
            elevator.transform.position = new Vector3(elevator.transform.position.x, elevator.transform.position.y + 0.2f, elevator.transform.position.z);
        }
        //去往二楼
        if (!stage && elevator.transform.position.y > down)
        {
            elevator.transform.position = new Vector3(elevator.transform.position.x, elevator.transform.position.y - 0.2f, elevator.transform.position.z);
        }
    }

    private void FixedUpdate()
    {
        if (boy.GetComponent<Controller>().isDoingAttack == 1 && boy.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            Debug.Log("BoyDoSwitch");
            if (System.Math.Abs(boy.transform.position.x - this.transform.position.x) <= 1)
            {
                Tools.GetComponent<Tools>().SendSwitchState(num, !stage);
            }

        }
        if (girl.GetComponent<Controller>().isDoingAttack == 1 && girl.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            Debug.Log("GirlDoSwitch");
            if (System.Math.Abs(girl.transform.position.x - this.transform.position.x) <= 1)
            {
                Tools.GetComponent<Tools>().SendSwitchState(num, !stage);
            }
            
        }
    }
}
