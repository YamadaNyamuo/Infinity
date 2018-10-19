using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpAlienBodyColCheck : MonoBehaviour {

    GameObject parent;
    WarpAlienMove warpAlienMove;
    BoxCollider2D col;

    // Use this for initialization
    void Start ()
    {
        parent = transform.parent.gameObject;
        col = GetComponent<BoxCollider2D>();
        warpAlienMove = parent.GetComponent<WarpAlienMove>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(warpAlienMove.state == WarpAlienMove.WARP_ALIEN_STATE.WARP && warpAlienMove.warpFlag == false) // ワープ状態でフェードアウトしている時
        {
            // 判定を一時的になくす
            col.enabled = false;
        }
        else if (warpAlienMove.state == WarpAlienMove.WARP_ALIEN_STATE.WARP && warpAlienMove.warpFlag == true) // ワープ状態でフェードインしている時
        {
            // 判定を元に戻す
            col.enabled = true;
        }
    }
}
