using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSearchCharacter : MonoBehaviour
{
    void Start()
    {

    }
    void OnTriggerStay2D(Collider2D col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //Debug.Log("OK");
            //敵キャラクターの状態を取得
            AlienMove.ALIEN_STATE state = GetComponentInParent<AlienMove>().GetState();
            //敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state != AlienMove.ALIEN_STATE.CHASE)
            {
                Debug.Log("プレイヤー発見");
                GetComponentInParent<AlienMove>().SetState(AlienMove.ALIEN_STATE.CHASE, col.transform);
            }
        }
    }
}
