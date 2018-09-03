using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsContorl_2_1: MonoBehaviour {
    public GameObject boy;//男孩
    public GameObject girl;//女孩
    public GameObject wall_1_off_top;//第一堵上侧的关着的墙
    public GameObject wall_1_on_top;//第一堵上侧的开着的墙
    public GameObject wall_2_off_top;//第二堵上侧的关着的墙
    public GameObject wall_2_on_top;//第二堵上侧的开着的墙
    public GameObject wall_3_off_top;//第三堵上侧的关着的墙
    public GameObject wall_3_on_top;//第三堵上侧的开着的墙

    public GameObject wall_1_off_below;//第一堵下侧的关着的墙
    public GameObject wall_1_on_below;//第一堵下侧的开着的墙
    public GameObject wall_2_off_below;//第二堵下侧的关着的墙
    public GameObject wall_2_on_below;//第二堵下侧的开着的墙
    public GameObject wall_3_off_below;//第三堵下侧的关着的墙
    public GameObject wall_3_on_below;//第三堵下侧的开着的墙

    void Start()
    {
        //初始化，令所有的墙关闭
        wall_1_off_top.SetActive(true);
        wall_1_on_top.SetActive(false);
        wall_2_off_top.SetActive(true);
        wall_2_on_top.SetActive(false);
        wall_3_off_top.SetActive(true);
        wall_3_on_top.SetActive(false);

        wall_1_off_below.SetActive(true);
        wall_1_on_below.SetActive(false);
        wall_2_off_below.SetActive(true);
        wall_2_on_below.SetActive(false);
        wall_3_off_below.SetActive(true);
        wall_3_on_below.SetActive(false);
    }

    void Update()
    {
        //如果小男孩和小女孩同时站在第一堵墙前，该墙消失
        if (System.Math.Abs(wall_1_on_below.transform.position.x - boy.transform.position.x) <= 1.5 && System.Math.Abs(wall_1_on_top.transform.position.x - girl.transform.position.x) <= 1.5)
        {
            wall_1_off_top.SetActive(false);
            wall_1_on_top.SetActive(true);
            wall_1_off_below.SetActive(false);
            wall_1_on_below.SetActive(true);
        }
        //如果小男孩和小女孩同时站在第二堵墙前，该墙消失
        if (System.Math.Abs(wall_2_on_below.transform.position.x - boy.transform.position.x) <= 1.5 && System.Math.Abs(wall_2_on_top.transform.position.x - girl.transform.position.x) <= 1.5)
        {
            wall_2_off_top.SetActive(false);
            wall_2_on_top.SetActive(true);
            wall_2_off_below.SetActive(false);
            wall_2_on_below.SetActive(true);
        }
        //如果小男孩和小女孩同时站在第三堵墙前，该墙消失
        if (System.Math.Abs(wall_3_on_below.transform.position.x - boy.transform.position.x) <= 1.5 && System.Math.Abs(wall_3_on_top.transform.position.x - girl.transform.position.x) <= 1.5)
        {
            wall_3_off_top.SetActive(false);
            wall_3_on_top.SetActive(true);
            wall_3_off_below.SetActive(false);
            wall_3_on_below.SetActive(true);
        }
    }

}

