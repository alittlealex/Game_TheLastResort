using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialization : MonoBehaviour {

    public GameObject info;
    public GameObject host;
    public GameObject props;

    void Start () {
        host.SetActive(true);
        info.SetActive(false);
        props.SetActive(false);
    }

	void Update () {
		
	}
}
