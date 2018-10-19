using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAlienGroundRay : MonoBehaviour
{

    Rigidbody2D rb;
    GameObject parent;
    WarpAlienMove warpAlienMove;

    // Use this for initialization
    void Start()
    {
        parent = transform.parent.gameObject;
        rb = parent.GetComponent<Rigidbody2D>();
        warpAlienMove = parent.GetComponent<WarpAlienMove>();
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
            //Debug.Log(hit.collider);
            if (warpAlienMove.state != WarpAlienMove.WARP_ALIEN_STATE.CHASE)
            {
                warpAlienMove.speed = 0;
            }
            // ワープ状態以外の時に落ちそうになったら加速を止める
            if (warpAlienMove.state != WarpAlienMove.WARP_ALIEN_STATE.WARP)
            {
                rb.velocity = Vector3.zero;
            }
        }
        else if (hit.collider != null)
        {
            warpAlienMove.speed = 7;
        }
    }
}
