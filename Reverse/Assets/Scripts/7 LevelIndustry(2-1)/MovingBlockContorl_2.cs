using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*移动的砖块的控制*/
/*第二幕*/
/*yellow_block_2、buts_1、buts_2的移动*/
public class MovingBlockContorl_2 : MonoBehaviour {

    public GameObject border_1;//buts的左限
    public GameObject border_2;//buts的右限
    

    public GameObject border_5;//buts_2的左限
    public GameObject border_6;//buts_2的右限

    //要移动的三个物体,buts、buts_2为左右移动，yellow_blocl_2为上下移动
    public GameObject buts;
    public GameObject buts_2;

    private bool v1;//true为向下，false为向上
    private bool v2;//true为向右，false为向左
    private bool v3;//true为向右，false为向左

    void Start () {
		
	}
	

	void Update () {
        /*buts_1的左右移动*/
        if (buts.transform.position.x <= border_1.transform.position.x + 2)
        {
            //若达到左限，改变方向为向右
            v2 = true;
        }
        if (buts.transform.position.x >= border_2.transform.position.x -2)
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

        /*buts_2的左右移动*/
        if (buts_2.transform.position.x <= border_5.transform.position.x)
        {
            //若达到左限，改变方向为向右
            v3 = true;
        }
        if (buts_2.transform.position.x >= border_6.transform.position.x - 1)
        {
            //若达到右限，改变方向为向左
            v3 = false;
        }
        //按照方向移动
        if (v3)
        {
            buts_2.transform.position += Vector3.right * Time.deltaTime * 1;
        }
        else
            buts_2.transform.position += Vector3.left * Time.deltaTime * 1;
    }
}
