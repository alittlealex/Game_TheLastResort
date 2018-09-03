using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hole : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
	}

    //如果掉坑里了，本关重启
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "PlayerBoy" || collision.gameObject.tag == "PlayerGirl")
        {
            collision.gameObject.GetComponent<Controller>().dead = true;
        }
    }
    
}
