using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Info_Next : MonoBehaviour {

    bool sex;//true表示男孩,false表示女孩
    public GameObject boy;
    public GameObject girl;
    public GameObject boyCV;
    public GameObject girlCV;

    void Start () {
        sex = true;
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        boy.SetActive(true);
        boyCV.SetActive(true);
        girl.SetActive(false);
        girlCV.SetActive(false);
    }
	
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
