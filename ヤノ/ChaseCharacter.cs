using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseCharacter : MonoBehaviour {
    void OnTriggerExit2D(Collider2D col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //Debug.Log("OK");
            //敵キャラクターの状態を取得
            EnemyMove.ENEMY_STETA state = GetComponentInParent<EnemyMove>().GetState();
            //敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state == EnemyMove.ENEMY_STETA.CHASE)
            {
                Debug.Log("プレイヤーを見失う");
                GetComponentInParent<EnemyMove>().SetState(EnemyMove.ENEMY_STETA.STOP, null);
            }
        }
    }
}
