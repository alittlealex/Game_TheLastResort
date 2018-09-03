using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour {
    public int num;
    public GameObject calculator_0;
    public GameObject calculator_1;
    public GameObject boy;
    public GameObject girl;
    public bool result;//true表示为1，false表示为0
    private float tempx;
    private float tempy;
    public static int boyCount = 0;
    public static int girlCount = 0;
    public GameObject Tools;
    // Use this for initialization
    void Start () {
        result = false;
        calculator_0.SetActive(true);
        calculator_1.SetActive(false);
        tempx = calculator_0.transform.position.x;
        tempy = calculator_0.transform.position.y;
        Tools = GameObject.Find("Tools");
    }
	
	// Update is called once per frame
	void Update () {
        //判断是否与数据一致
        if (CaculatorController.result.results[num - 1] == -1)
        {
            result = false;
        }
        else
        {
            result = true;
        }
        //
        //女
        

        if (result)
        {
            calculator_1.SetActive(true);
            calculator_0.SetActive(false);
        }
        else
        {           
            calculator_0.SetActive(true);
            calculator_1.SetActive(false);
        }

    }

    private void FixedUpdate()
    {
        if (girl.GetComponent<Controller>().isDoingAttack == 1 && girl.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            girlCount++;
            if (System.Math.Abs(girl.transform.position.x - tempx) <= 0.5 && System.Math.Abs(girl.transform.position.y - tempy) <= 2)
            {
                if (!result)
                    Tools.GetComponent<Tools>().SendCalculatorResultChangeReq(1, num, CaculatorController.result);
                else
                    Tools.GetComponent<Tools>().SendCalculatorResultChangeReq(-1, num, CaculatorController.result);
            }
            
        }
        //男
        if (boy.GetComponent<Controller>().isDoingAttack == 1 && boy.GetComponent<Controller>().gender == Network.m_Actor.gender)
        {
            boyCount++;
            if (System.Math.Abs(boy.transform.position.x - tempx) <= 0.5 && System.Math.Abs(boy.transform.position.y - tempy) <= 2)
            {
                if (!result)
                    Tools.GetComponent<Tools>().SendCalculatorResultChangeReq(1, num, CaculatorController.result);
                else
                    Tools.GetComponent<Tools>().SendCalculatorResultChangeReq(-1, num, CaculatorController.result);
            }
            
        }
    }
}
