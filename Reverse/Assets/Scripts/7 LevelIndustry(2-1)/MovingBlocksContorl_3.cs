using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlocksContorl_3 : MonoBehaviour {

    /*控制移动砖块的移动*/
    /*第四幕*/
    public GameObject yellow;
    public GameObject border_1;//yellow的上限
    public GameObject border_2;//yellow的下限

    public GameObject buts;
    public GameObject border_3;//buts的左限
    public GameObject border_4;//buts的右限
    private bool v1;//true为向下，false为向上
    private bool v2;//true为向右，false为向左

    void Start () {
		
	}
	

	void Update () {
        /*yellow_block_2的上下移动*/
        if (yellow.transform.position.y >= border_1.transform.position.y)
        {
            //若达到上限，改变方向为向下
            v1 = true;
        }
        if (yellow.transform.position.y <= border_2.transform.position.y)
        {
            //若达到下限，改变方向为向下
            v1 = false;
        }
        //按照方向移动
        if (v1)
        {
            yellow.transform.position += Vector3.down * Time.deltaTime * 1;
        }
        else
            yellow.transform.position += Vector3.up * Time.deltaTime * 1;

        /*buts_1的左右移动*/
        if (buts.transform.position.x <= border_3.transform.position.x+1 )
        {
            //若达到左限，改变方向为向右
            v2 = true;
        }
        if (buts.transform.position.x >= border_4.transform.position.x-1 )
        {
            //若达到右限，改变方向为向左
            v2 = false;
        }
        //按照方向移动
        if (v2)
        {
            buts.transform.position += Vector3.right * Time.deltaTime * 1;
        }
        else
            buts.transform.position += Vector3.left * Time.deltaTime * 1;
    }
}
