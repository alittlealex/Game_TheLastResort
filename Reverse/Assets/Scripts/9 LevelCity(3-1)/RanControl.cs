using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanControl : MonoBehaviour {

    public GameObject rain;
	// Use this for initialization
	void Start () {
        

    }

    // Update is called once per frame
    void Update () {
        foreach (Transform child in gameObject.transform)
        {
            if(child.transform.position.y>=143)
            {
                child.transform.position = new Vector3(child.transform.position.x, child.transform.position.y - 0.2f, 0);
            }
            else
            {
                child.transform.position = new Vector3(child.transform.position.x, 156, 0);
            }
        }
    }
}
