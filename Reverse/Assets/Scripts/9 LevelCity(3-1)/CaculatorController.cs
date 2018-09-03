using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaculatorResult
{
    public int[] results = new int[6];
    public CaculatorResult()
    {
        for (int i = 0; i < 6; i++)
        {
            results[i] = -1;
        }
    }

    public static bool operator==(CaculatorResult a,CaculatorResult b)
    {
        bool flag = true;
        for(int i = 0; i < 6; i++)
        {
            if (a.results[i] != b.results[i])
                flag = false;
        }
        return flag;
    }

    public static bool operator !=(CaculatorResult a, CaculatorResult b)
    {
        bool flag = false;
        for (int i = 0; i < 6; i++)
        {
            if (a.results[i] != b.results[i])
                flag = true;
        }
        return flag;
    }
}

public class CaculatorController : MonoBehaviour {

    private GameObject Tools;
    public CaculatorResult newResult = new CaculatorResult();
    public static CaculatorResult result = new CaculatorResult();

    void Start()
    {
        Tools = GameObject.Find("Tools");
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        //得到新ack
        if (Network.m_Actor.isNewResult)
        {
            newResult = ComputeResult(Network.m_Actor.calculatorResultChange_Ack);
            
        }
        //赋值新ack
        if(newResult != result)
        {
            for (int i = 0; i < 6; i++)
            {
                result.results[i] = newResult.results[i];
            }
            Network.m_Actor.isNewResult = false;
        }
    }

    CaculatorResult ComputeResult(mmopb.CalculatorResultChange_ack ack)
    {
        CaculatorResult ret = new CaculatorResult();
        ret.results[0] = ack.cal_1;
        ret.results[1] = ack.cal_2;
        ret.results[2] = ack.cal_3;
        ret.results[3] = ack.cal_4;
        ret.results[4] = ack.cal_5;
        ret.results[5] = ack.cal_6;
        return ret;
    }
}
