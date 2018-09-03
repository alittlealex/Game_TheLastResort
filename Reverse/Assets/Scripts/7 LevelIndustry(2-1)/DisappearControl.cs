using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*令不稳定的灰色砖块逐渐消失*/
public class DisappearControl : MonoBehaviour {
    public GameObject block;//不稳定的灰色砖块
    public GameObject player;//玩家
    float c;//透明度
    private bool flag;//true表示已经启动，false表示未启动

	void Start () {
        //初始化透明度为原来的值
        c = block.GetComponent<SpriteRenderer>().color.a;
        flag = false;
    }
	
	
	void Update () {
        //如果砖块自毁已经启动，则逐渐降低透明度，直到接近0时彻底消失
        if(flag)
            if (c >= 0.01f)
            {
                block.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, c);
                c = c - 0.01f;
            }
            else
            {
                block.SetActive(false);
            }
	}
 //如果玩家站在该砖块的上方，启动砖块自毁
    private void OnCollisionEnter2D(Collision2D collision)
    {
        flag = true;
    }
}
