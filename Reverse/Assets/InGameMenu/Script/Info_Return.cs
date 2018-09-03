using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Info_Return : MonoBehaviour {

    public GameObject info;
    public GameObject host;

    void Start () {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnClick()
    {
        info.SetActive(false);
        host.SetActive(true);
    }
}
