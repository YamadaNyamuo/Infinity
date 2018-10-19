using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPLSearch : MonoBehaviour
{

    public bool plSearchFlag = false; // プレイヤーが範囲内にいるかどうかのフラグ
    float waitTimer;                  // 待機時間

    // Use this for initialization
    void Start()
    {
        waitTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (plSearchFlag == false)
            {
                plSearchFlag = true;
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (plSearchFlag == false)
            {
                Count();
            }
        }
    }

    // 初期位置に戻った直後、プレイヤーが真下に居たらすぐに落ちてきてしまうので1秒待つ
    void Count()
    {
        waitTimer += Time.deltaTime;

        // 初期位置に戻ってから1秒経ったら落下
        if (waitTimer >= 1.0f)
        {
            plSearchFlag = true;
            waitTimer = 0;
        }
    }
}
