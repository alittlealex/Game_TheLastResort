using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobortControl : MonoBehaviour {

    public GameObject robort;
    public GameObject wall_left;
    public GameObject wall_right;
    public GameObject boy;
    public GameObject girl;
    public bool v;//速度方向，为true时向右，为false时向左
    public bool on;//是否启动，为true时启动，为false时不启动
    // Use this for initialization
    void Start () {
        v = false;
        on = false;
        robort.transform.position = new Vector3(wall_right.transform.position.x, 145.9f, 0);
	}
	
	// Update is called once per frame
	void Update () {
        if (on)
        {
            if (System.Math.Abs(robort.transform.position.x - wall_left.transform.position.x) <= 1)
            {
                v = true;
                robort.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (System.Math.Abs(robort.transform.position.x - wall_right.transform.position.x) <= 1)
            {
                 v = false;
                 robort.GetComponent<SpriteRenderer>().flipX = true;
            }
            if(v)
            {
                robort.transform.position = new Vector3(robort.transform.position.x + 0.1f, 145.9f, 0);
            }
            else
            {
                robort.transform.position = new Vector3(robort.transform.position.x - 0.1f, 145.9f, 0);
            }
            Hurt();
            
        }
    }
    public void Hurt()
    {
        float x, y;
        GameObject player;
        player = boy;
        x = robort.transform.position.x - player.transform.position.x;
        y = robort.transform.position.y - player.transform.position.y;
        if(System.Math.Sqrt(x*x+y*y)<=1.2f)
        {
            boy.GetComponent<SelfController>().setDead();
        }
        player = girl;
        x = robort.transform.position.x - player.transform.position.x;
        y = robort.transform.position.y - player.transform.position.y;
        if (System.Math.Sqrt(x * x + y * y) <= 1.2f)
        {
            girl.GetComponent<SelfController>().setDead();
        }
    }
}
