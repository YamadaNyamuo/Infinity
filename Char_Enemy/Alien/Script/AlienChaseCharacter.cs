using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienChaseCharacter : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject parent;

    void Start()
    {
        parent = transform.parent.gameObject;
        rb = parent.GetComponent<Rigidbody2D>();
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //Debug.Log("OK");
            //敵キャラクターの状態を取得
            AlienMove.ALIEN_STATE state = GetComponentInParent<AlienMove>().GetState();
            if (state == AlienMove.ALIEN_STATE.CHASE)
            {
                Debug.Log("プレイヤーを見失う");
                GetComponentInParent<AlienMove>().speed = 0;
                rb.velocity = Vector3.zero;
                GetComponentInParent<AlienMove>().SetState(AlienMove.ALIEN_STATE.STOP, null);
                GetComponentInParent<AlienMove>().timeElapsed = 0;
            }
        }
    }
}
