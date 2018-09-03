using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyshipControl : MonoBehaviour {

    public GameObject ship;
	// Use this for initialization
	void Start () {
        ship.transform.position = new Vector3(192, 148.76f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if(ship.transform.position.x<=230)
            ship.transform.position = new Vector3(ship.transform.position.x+0.03f, 148.76f, 0);
        else
            ship.transform.position = new Vector3(192, 148.76f, 0);
    }
}
