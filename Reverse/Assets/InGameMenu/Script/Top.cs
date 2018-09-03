using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Top : MonoBehaviour
{

    public bool Flag_Appear = false;
    public GameObject B_info;
    //public GameObject B_replay;
    public GameObject B_title;
    public GameObject B_ret;
    public GameObject T_menu;
    public GameObject host_background;

    void Start()
    {
        host_background.SetActive(false);
        DisAppear();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Invoke("WindowMove", 0.0001f);
            Flag_Appear = !Flag_Appear;
        }
        if (Flag_Appear)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void DisAppear()
    {
        B_info.SetActive(false);
        //B_replay.SetActive(false);
        B_title.SetActive(false);
        B_ret.SetActive(false);
        T_menu.SetActive(false);
    }

    private void Appear()
    {   
        B_info.SetActive(true);
        //B_replay.SetActive(true);
        B_title.SetActive(true);
        B_ret.SetActive(true);
        T_menu.SetActive(true);
    }

    public void WindowMove()
    {
        if (host_background.transform.localPosition.y > 8956 && Flag_Appear)
        {
            host_background.transform.Translate(Vector3.down * 5f, Space.World);
            host_background.SetActive(true);
            DisAppear();
            Invoke("WindowMove", 0.0001f);
        }
        else if (host_background.transform.localPosition.y < 9325 && !Flag_Appear)
        {
            host_background.transform.Translate(Vector3.up * 5f, Space.World);
            host_background.SetActive(true);
            DisAppear();
            Invoke("WindowMove", 0.0001f);
        }
        else if (Flag_Appear)
            Appear();
        else
            host_background.SetActive(false);
    }
}
