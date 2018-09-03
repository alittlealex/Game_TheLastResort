using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Host : MonoBehaviour {

    public GameObject info;
    public GameObject host;
    public GameObject props;
    // Use this for initialization
    void Start () {
        //info = GameObject.Find("Info");
        //host = GameObject.Find("Host");
        //props = GameObject.Find("Props");
        host.SetActive(true);
        info.SetActive(false);
        props.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
