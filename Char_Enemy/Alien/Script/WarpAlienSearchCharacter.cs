using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAlienSearchCharacter : MonoBehaviour
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
            WarpAlienMove.WARP_ALIEN_STATE state = GetComponentInParent<WarpAlienMove>().GetState();
            //敵キャラクターが追いかける状態でなければ追いかける設定に変更
            if (state != WarpAlienMove.WARP_ALIEN_STATE.CHASE && state != WarpAlienMove.WARP_ALIEN_STATE.WARP)
            {
                Debug.Log("プレイヤー発見");
                GetComponentInParent<WarpAlienMove>().SetState(WarpAlienMove.WARP_ALIEN_STATE.CHASE, col.transform);
            }
        }
    }
}