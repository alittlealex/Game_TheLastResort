using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLight : MonoBehaviour {


    public GameObject boy;
    public GameObject control;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name.Equals("PlayerBoy"))
        {
            boy.GetComponent<Controller>().dead = true;
        }
    }
}
