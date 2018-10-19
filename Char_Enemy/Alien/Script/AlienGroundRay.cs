using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGroundRay : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject parent;
    AlienMove alienMove;

    // Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject;
        rb = parent.GetComponent<Rigidbody2D>();
        alienMove = parent.GetComponent<AlienMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, -1, 0), 1.63f);

        // 可視化
        Debug.DrawRay(pos, new Vector3(0, -1.63f, 0), Color.blue, 1);
        //Debug.Log(hit.collider);

        // 進む先に何もなかったら
        if (hit.collider == null)
        {
            alienMove.speed = 0;
            rb.velocity = Vector3.zero;
        }
        else if (hit.collider != null && alienMove.state != AlienMove.ALIEN_STATE.STOP) // 俺は止まんねぇからよ…している時以外
        {
            alienMove.speed = 7;
        }
    }
}

