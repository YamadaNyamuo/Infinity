using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAlienChaseCharacter : MonoBehaviour
{
    void Start()
    {

    }

    void OnTriggerExit2D(Collider2D col)
    {
        //　プレイヤーキャラクターを発見
        if (col.tag == "Player")
        {
            //Debug.Log("OK");
            //敵キャラクターの状態を取得
            WarpAlienMove.WARP_ALIEN_STATE state = GetComponentInParent<WarpAlienMove>().GetState();
            // プレイヤーを見失った場合、ワープに移行
            if (state == WarpAlienMove.WARP_ALIEN_STATE.CHASE)
            {
                Debug.Log("プレイヤーを見失う");
                GetComponentInParent<WarpAlienMove>().SetState(WarpAlienMove.WARP_ALIEN_STATE.WARP, null);
                GetComponentInParent<WarpAlienMove>().timeElapsed = 0;
            }
        }
    }
}
