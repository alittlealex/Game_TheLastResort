using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*火焰控制*/
public class FireControl : MonoBehaviour {
    void Start () {
    }
	
	
	void Update () {
        //this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        //每帧切换X方向，实现火焰的跳动
    }

    /*触发器*/
    /*当男孩或女孩进入火焰时，死亡*/
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerGirl")
        {
            collision.gameObject.GetComponent<Controller>().dead = true;
        }
        if (collision.gameObject.tag == "PlayerBoy")
        {
            collision.gameObject.GetComponent<Controller>().dead = true;
        }
    }
}
