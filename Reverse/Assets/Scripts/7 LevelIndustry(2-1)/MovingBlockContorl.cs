using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*移动的砖块的控制*/
/*第一幕*/
public class MovingBlockContorl : MonoBehaviour {

    public GameObject yellow_block_1;//左右移动的黄色砖块
    public GameObject block_1;//黄色砖块移动的上界
    public GameObject block_2;//黄色砖块移动的下界
    public GameObject block_3;//绿色砖块移动的左界
    public GameObject block_4;//绿色砖块移动的右界
    public GameObject buts;//左右移动的绿色砖块
    private bool v1;//true为向下，false为向上
    private bool v2;//true为向右，false为向左
                   
    void Start () {

	}
	
	void Update () {
        //控制黄色的砖块在block_1与block_2之间左右移动
        if (yellow_block_1.transform.position.y>= block_1.transform.position.y )
        {
            //若到达上界，改变方向为下
            v1 = true;
        }
        if (yellow_block_1.transform.position.y <= block_2.transform.position.y-0.5)
        {
            //若到达下界，改变方向为上
            v1 = false;
        }

        //按照方向移动
        if (v1)
        {
            yellow_block_1.transform.position += Vector3.down * Time.deltaTime * 1;
        }
        else
            yellow_block_1.transform.position += Vector3.up * Time.deltaTime * 1;

        //控制绿色的砖块在block_3与block_4之间左右移动
        if (buts.transform.position.x <= block_3.transform.position.x+1)
        {
            //若到达左界，改变方向向右
            v2 = true;
        }
        if (buts.transform.position.x >= block_4.transform.position.x )
        {
            //若到达右界，改变方向向左
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
