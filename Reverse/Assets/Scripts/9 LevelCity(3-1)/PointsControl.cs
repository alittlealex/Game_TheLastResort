using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsControl : MonoBehaviour {

    public GameObject boy;
    public GameObject girl;
    public GameObject wall_1_off;
    public GameObject wall_1_on;
    public GameObject wall_2_off;
    public GameObject wall_2_on;
    public GameObject wall_3_off;
    public GameObject wall_3_on;
    public GameObject wall_4_off;
    public GameObject wall_4_on;
    // Use this for initialization
    void Start () {
        wall_1_off.SetActive(true);
        wall_1_on.SetActive(false);
        wall_2_off.SetActive(true);
        wall_2_on.SetActive(false);
        wall_3_off.SetActive(true);
        wall_3_on.SetActive(false);
        wall_4_off.SetActive(true);
        wall_4_on.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if(System.Math.Abs(wall_1_on.transform.position.x-boy.transform.position.x)<= 1.5 && System.Math.Abs(wall_1_on.transform.position.x - girl.transform.position.x) <= 1.5)
        {
                    wall_1_off.SetActive(false);
                    wall_1_on.SetActive(true);
        }
        if (System.Math.Abs(wall_2_on.transform.position.x - boy.transform.position.x) <= 1.5 && System.Math.Abs(wall_2_on.transform.position.x - girl.transform.position.x) <= 1.5)
        {       
                    wall_2_off.SetActive(false);
                    wall_2_on.SetActive(true);
        }
        if (System.Math.Abs(wall_3_on.transform.position.x - boy.transform.position.x) <= 1.5 && System.Math.Abs(wall_3_on.transform.position.x - girl.transform.position.x) <= 1.5)
        {      
                    wall_3_off.SetActive(false);
                    wall_3_on.SetActive(true);
        }
        if (System.Math.Abs(wall_4_on.transform.position.x - boy.transform.position.x) <= 1.5 && System.Math.Abs(wall_4_on.transform.position.x - girl.transform.position.x) <= 1.5)
        {
                    wall_4_off.SetActive(false);
                    wall_4_on.SetActive(true);
        }
     
        
	}

}
