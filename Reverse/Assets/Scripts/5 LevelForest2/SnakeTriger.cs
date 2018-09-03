using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class SnakeTriger : MonoBehaviour {

    public GameObject playerGirl;
    public GameObject snake;
    public GameObject GirlDialog1;
    public PlayableDirector mDirector;
    public float snakeSpeed = 3;
    //女孩是否死了
    bool isTrigered = false;
    //男孩是否可以开始救女孩了
    bool isBoy = false;
    //男孩是否已经救了女孩了
    bool isBoyIn = false;
    //蛇走过的距离
    float dis = 0;
    //是否开始显示对话
    bool isShow = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isBoyIn && !isShow)
        {
            isShow = true;
            
            mDirector.Play();
        }
        if(isBoyIn)
        {
            SnakeIn();
        }
	}

    ///如果妹子进入触发器，蛇就跑出来
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerGirl" && !isTrigered && !isBoyIn)
        {
            GirlDialog1.SetActive(true);
            playerGirl.GetComponent<Controller>().ctrlState = Controller.CtrlState.NonPlayer;
            playerGirl.GetComponent<Controller>().actor.walk = 0;
            playerGirl.GetComponent<Animator>().SetTrigger("Hurt");
            isBoy = true;
            SnakeOut();
        }
        if (isBoy && collision.gameObject.tag == "PlayerBoy")
        {
            isBoyIn = true;
            playerGirl.GetComponent<Controller>().ctrlState = Controller.CtrlState.Player;
        }
        
    }

    /// <summary>
    /// 蛇出动
    /// </summary>
    private void SnakeOut()
    {
        if (dis < 5)
        {
            Debug.Log(dis);
            snake.transform.position -= new Vector3(snakeSpeed * Time.deltaTime, 0, 0);
            dis += snakeSpeed * Time.deltaTime;
        }
        else
        {
            isTrigered = true;
            playerGirl.GetComponent<Controller>().dead = true;
        }

    }

    /// <summary>
    /// 蛇回去
    /// </summary>
    private void SnakeIn()
    {
        snake.GetComponent<SpriteRenderer>().flipX = true;
        snake.transform.position += new Vector3(snakeSpeed * Time.deltaTime * 2, 0, 0);
    }

}
