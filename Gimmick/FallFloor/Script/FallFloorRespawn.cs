using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloorRespawn : MonoBehaviour
{
    Vector2 startPosition;  // 初期位置
    FallAreaPLSearch fallAreaPLSearch;
    Rigidbody2D rb;

    public float respawnTime = 2f; // 再出減までにかかる時間

    GameObject parent;

    // Use this for initialization

    void Awake()
    {
        fallAreaPLSearch = transform.parent.Find("FallArea").GetComponent<FallAreaPLSearch>();
        parent = transform.parent.gameObject;
        rb = parent.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPosition = parent.transform.position;
        //parent = transform.parent.gameObject;;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
           parent.SetActive(false);
           fallAreaPLSearch.fallFlag = false;
           rb.bodyType = RigidbodyType2D.Kinematic;
           rb.gravityScale = 0;
        }
    }

    void ReSpawnFloor()
    {
        parent.transform.position = startPosition;
        parent.SetActive(true);
    }

    // 非アクティブにして2秒後に再出現
    void OnDisable()
    {
        Invoke("ReSpawnFloor", respawnTime);
    }
}
