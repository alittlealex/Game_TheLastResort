using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*涡轮控制*/
public class WolunControl : MonoBehaviour {

	void Start () {
		
	}
	

	void Update () {
        //涡轮自转
        this.transform.Rotate(Vector3.back * 5, Space.Self);
    }

    //当有玩家进入涡轮时死亡
    private void OnTriggerStay2D(Collider2D player)
    {
        if (player.gameObject.tag == "PlayerBoy" || player.gameObject.tag == "PlayerGirl")
            player.GetComponent<Controller>().dead = true;
        
    }
}
