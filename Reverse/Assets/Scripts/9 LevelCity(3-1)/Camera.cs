using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {
    public GameObject boy;
    public GameObject girl;
    public GameObject c;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

            c.transform.position=new Vector3((boy.transform.position.x+girl.transform.position.x)/2, 149.09f, -10);

	}
}
