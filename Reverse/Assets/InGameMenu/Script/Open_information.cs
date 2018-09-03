using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Open_information : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject host;
    public GameObject info;
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        this.GetComponentInChildren<Text>().fontSize = 14;
        this.GetComponentInChildren<Text>().color = Color.black;
        info.SetActive(false);
    }

    void Update()
    {

    }
    private void OnClick()
    {
        this.GetComponentInChildren<Text>().color = Color.white;
        Invoke("LaunchProjectile", 0/*0.5f*/);
    }
    private void LaunchProjectile()
    {
        this.GetComponentInChildren<Text>().color = Color.black;
        info.SetActive(true);
        host.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = 20;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponentInChildren<Text>().fontSize = 14;
    }
}
