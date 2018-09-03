using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairMove : MonoBehaviour {

    public float moveSpeed = 5;
    public float timeThreshold = 2;
    private float timer;
    //方向
    private bool isUp;

	void Start ()
    {
        isUp = true;
        timer = 0;
	}
	
	void Update ()
    {
        if (isUp)
        {
            timer += Time.deltaTime;
            if (timer >= timeThreshold)
                isUp = !isUp;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                isUp = !isUp;
        }
    }

    private void FixedUpdate()
    {
        if(isUp)
        {
            transform.position = transform.position + Vector3.up * Time.deltaTime * moveSpeed; 
        }
        else
        {
            transform.position = transform.position - Vector3.up * Time.deltaTime * moveSpeed;
        }
    }

}
