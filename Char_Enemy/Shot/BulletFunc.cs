using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFunc : MonoBehaviour {

    private Rigidbody2D rb2d;
    private Vector3 PPos;

    //これ（弾）自体の位置座標
    Vector3 Position;
    //弾の速さ
    public float SPEED;
    //プレイヤーの情報　これの発射元から取ってきてる
    GameObject Player;
    //プレイヤーと飛んでる敵の間の距離をｘ軸とy軸に分けてある
    float tX;
    float tY;
    float alfa;
    Vector3 angle;
    //親のオブジェクト
    private GameObject parent;
    ShotFunc p;

    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player");
        Position = transform.position;
        angle = (PPos - transform.position);
        angle = angle / angle.magnitude;
        parent = transform.root.gameObject;
        p = parent.GetComponent<ShotFunc>();
        p.Flag(true);
        //スピードはプレイヤー参照して変える
        SPEED = p.shotSpeed;
    }
	
	// Update is called once per frame
	void Update () {
    ShotFunc p = parent.GetComponent<ShotFunc>();

        //画面外に出たらこのオブジェクトを消す
        if (!GetComponent<Renderer>().isVisible)
        {
            Debug.Log("hakugyoukurou");
            p.Flag(false);
            Destroy(this.gameObject);
        }
        else
        {
            //処理なし
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(angle * SPEED * Time.deltaTime, Space.World);
    }

    public void Fire(Vector3 pos, Vector3 pPos)     //posはこの弾自体の位置 pPosはプレイヤーの位置
    {
        PPos = pPos;
        transform.position = pos;
    }
}
