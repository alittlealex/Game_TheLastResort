using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*控制摄像机的移动,坐标为男女孩横坐标的中点*/
public class Camera_2_1 : MonoBehaviour
{
    public GameObject boy;//男孩
    public GameObject girl;//女孩
    public GameObject c;//摄像机
  
    void Start()
    {
        
    }

    void Update()
    {
        /*控制摄像机的移动,坐标为男女孩横坐标的中点*/
        c.transform.position = new Vector3((boy.transform.position.x + girl.transform.position.x) / 2, 163.16f, -10);
    }
}
