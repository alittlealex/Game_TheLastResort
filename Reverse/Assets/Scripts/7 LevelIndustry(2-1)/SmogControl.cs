using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmogControl : MonoBehaviour {
    public GameObject boy;//男孩
    public GameObject girl;//女孩
    public GameObject smog;//烟雾
    public GameObject mask;//面具
    int boy_hp = 120;//男孩的HP
    int girl_hp = 120;//女孩的HP

	void Start () {
		
	}
	
	void Update () {
        /*当男孩的HP小于0时，男孩死亡*/
		if(boy_hp<=0)
        {
            boy.GetComponent<Controller>().dead = true;
        }
        /*当女孩的HP小于0时，女孩死亡*/
        if (girl_hp<=0)
        {
            girl.GetComponent<Controller>().dead = true;
        }
	}

    /*触发器*/
    private void OnTriggerStay2D(Collider2D other)
    {
        /*当小男孩进入烟雾并且没带面具时，HP--*/
        if(other.name.Equals("PlayerBoy")&&mask.GetComponent<MaskContorl>().owner!=1)
        {
            boy_hp--;
        }
        /*当小女孩进入烟雾并且没带面具时，HP--*/
        if (other.name.Equals("PlayerGirl") && mask.GetComponent<MaskContorl>().owner != 2)
        {
            girl_hp--;
        }
    }
}
