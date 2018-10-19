using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour {

    GameObject fallArea;
    FallAreaPLSearch fallSearch;

    Rigidbody2D rb;
    BoxCollider2D boxCol;

    // Use this for initialization
    void Start ()
    {
        fallArea = transform.FindChild("FallArea").gameObject;
        fallSearch = fallArea.GetComponent<FallAreaPLSearch>();
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fallSearch.fallFlag == true)
        {
            // nフレーム後に落下
            StartCoroutine(DelayMethod(3, () =>
            {
                Fall();
            }));
        }
    }

   //void OnCollisionEnter2D(Collision2D col)
   //{
   //     if (col.gameObject.tag == "Ground")
   //     {
   //         Destroy(this.gameObject);
   //     }
   //}


    void Fall()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 2;
    }

    private IEnumerator DelayMethod(int delayFrameCount, Action action)
    {
        for (var i = 0; i < delayFrameCount; i++)
        {
            yield return null;
        }
        action();
    }

    void OnBecameInvisible()
    {
        if (this.gameObject.activeSelf != false)
        {
            if (fallSearch.fallFlag == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
