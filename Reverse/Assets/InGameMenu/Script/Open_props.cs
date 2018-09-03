using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Open_props : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject props;
    public GameObject host;
    public GameObject info;
    Scene scene;
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        this.GetComponentInChildren<Text>().fontSize = 14;
        this.GetComponentInChildren<Text>().color = Color.black;
        props.SetActive(false);
    }

    void Update()
    {

    }
    private void OnClick()
    {
       // Debug.Log("Click");
        this.GetComponentInChildren<Text>().color = Color.white;
        //Invoke("LaunchProjectile", 0/*0.5f*/);
        SceneManager.LoadScene(scene.name);
    }
    private void LaunchProjectile()
    {
        this.GetComponentInChildren<Text>().color = Color.black;
        props.SetActive(true);
        host.SetActive(false);
        info.SetActive(false);
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
