using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Open_Return : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject topmenu;
    void Start()
    {
    
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        this.GetComponentInChildren<Text>().fontSize = 14;
        this.GetComponentInChildren<Text>().color = Color.black;
    }


    void Update()
    {

    }

    private void OnClick()
    {
        this.GetComponentInChildren<Text>().color = Color.white;
        Invoke("LaunchProjectile", 0/*0.5f*/);//5秒后调用LaunchProjectile () 函数
    }
    private void LaunchProjectile()
    {
        this.GetComponentInChildren<Text>().color = Color.black;
        topmenu.GetComponent<Top>().Flag_Appear = false;
        topmenu.GetComponent<Top>().WindowMove();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = 20;
        //当鼠标光标移入该对象时触发
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = 14;
        //当鼠标光标移出该对象时触发
    }
    
}

