using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Next : MonoBehaviour {

    // Use this for initialization
    bool sex;//true表示男孩,false表示女孩
    GameObject boy;
    GameObject girl;
    GameObject boyCV;
    GameObject girlCV;
    void Awake()
    {
        
    }
    void Start () {
        boy = GameObject.Find("boy_background");
        girl = GameObject.Find("girl_background");
        boyCV = GameObject.Find("boyCV");
        girlCV = GameObject.Find("girlCV");

        //Debug.Log("uisds");
        sex = true;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        boy.SetActive(true);
        boyCV.SetActive(true);
        girl.SetActive(false);
        girlCV.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnClick()
    {
        if(sex)
        {
            boy.SetActive(false);
            boyCV.SetActive(false);
            girl.SetActive(true);
            girlCV.SetActive(true);
        }
        else
        {
            boy.SetActive(true);
            boyCV.SetActive(true);
            girl.SetActive(false);
            girlCV.SetActive(false);
        }
        sex = !sex;

    }
}
