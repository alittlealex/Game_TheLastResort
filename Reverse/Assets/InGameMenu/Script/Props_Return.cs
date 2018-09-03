using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Props_Return : MonoBehaviour
{

    public GameObject props;
    public GameObject host;
    // Use this for initialization
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        //props = GameObject.Find("Props");
        //host = GameObject.Find("Host");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnClick()
    {
        props.SetActive(false);
        host.SetActive(true);
    }
}
