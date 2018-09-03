using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*面具控制*/
public class MaskContorl : MonoBehaviour {
    public GameObject boy;//小男孩
    public GameObject girl;//小女孩
    public GameObject mask;//面具
    public int owner;//0表示无主人，1表示主人为男孩，2表示主人为女孩
    private bool first_flag = false;//第一次按S时不交换，实际为捡起
    public GameObject Tools;

    private Vector2 maskPos;


	void Start () {
        owner = 0;//初始状态无主人
        Tools = GameObject.Find("Tools");
	}
	
	void Update () {
        if (Network.m_Actor.isReceiveMaskCheck)
        {
            owner = Network.m_Actor.maskCheckAck.gender;
            maskPos = new Vector2(Network.m_Actor.maskCheckAck.position.x, Network.m_Actor.maskCheckAck.position.y);
            if (!first_flag)
                first_flag = true;
            Network.m_Actor.isReceiveMaskCheck = false;
        }


        //若小男孩为主人，则面具位置跟随小男孩移动，当小男孩按下K，卸下面具并抛向地图下侧
		if(owner == 1)
        {
            mask.transform.position = new Vector3(boy.transform.position.x, boy.transform.position.y+0.3f, 0);
            if(boy.GetComponent<Controller>().isDoingAttack == 1)
            {
                Tools.GetComponent<Tools>().SendMaskCheckReq(0, new Vector2(boy.transform.position.x - 3, girl.transform.position.y));
            }
        }
        //若小女孩为主人，则面具位置跟随小女孩移动，当小女孩按下K，卸下面具并抛向地图上侧
        else if (owner == 2)
        {
            mask.transform.position = new Vector3(girl.transform.position.x, girl.transform.position.y+0.3f, 0);

            if (girl.GetComponent<Controller>().isDoingAttack == 1)
            {
                Tools.GetComponent<Tools>().SendMaskCheckReq(0, new Vector2(girl.transform.position.x - 3, boy.transform.position.y));
            }
        }
        else if(owner == 0 && first_flag)
        {
            mask.transform.position = new Vector3(maskPos.x, maskPos.y, 0);
        }
	}

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        //小男孩捡起面具
        if(collision.gameObject.tag == "PlayerBoy" && boy.GetComponent<Controller>().isDoingAttack == 1 && owner == 0)
        {
            Tools.GetComponent<Tools>().SendMaskCheckReq(Network.m_Actor.gender, new Vector2(boy.transform.position.x, boy.transform.position.y + 0.3f));
            boy.GetComponent<Controller>().isDoingAttack = -1;
        }

        //小女孩捡起面具
        if (collision.gameObject.tag == "PlayerGirl" && girl.GetComponent<Controller>().isDoingAttack == 1 && owner == 0)
        {
            Tools.GetComponent<Tools>().SendMaskCheckReq(Network.m_Actor.gender, new Vector2(girl.transform.position.x, girl.transform.position.y + 0.3f));
            girl.GetComponent<Controller>().isDoingAttack = -1;
        }
    }
}
