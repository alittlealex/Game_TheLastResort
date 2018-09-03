using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalulatorContorl : MonoBehaviour {
    public GameObject wall;
    public GameObject C5;
    public GameObject C4;
    public GameObject C3;
    public GameObject C2;
    public GameObject C1;
    public GameObject C0;
    public GameObject book;
    private int cnt;
    // Use this for initialization
    void Start () {
        cnt = 0;
	}
	
	// Update is called once per frame
	void Update () {
        cnt = 0;
        if(C0.GetComponent<Calculator>().result)
        {
            cnt += 1;
        }
        if (C1.GetComponent<Calculator>().result)
        {
            cnt += 2;
        }
        if (C2.GetComponent<Calculator>().result)
        {
            cnt += 4;
        }
        if (C3.GetComponent<Calculator>().result)
        {
            cnt += 8;
        }
        if (C4.GetComponent<Calculator>().result)
        {
            cnt += 16;
        }
        if (C5.GetComponent<Calculator>().result)
        {
            cnt += 32;
        }
        Debug.Log("random" + book.GetComponent<BookControl>().randnum);
        if (cnt==book.GetComponent<BookControl>().randnum && cnt!=0)
        {
            
            wall.SetActive(false);
        }
        else
        {
            Debug.Log(cnt);
        }
    }
}
