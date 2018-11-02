using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFunc : MonoBehaviour {

    //敵の攻撃フラグ
    public bool EAtackFlag;
    //敵自身が弾を出してる最中かどうか調べるフラグ
    public bool EBulletFlag;

    //プレイヤーの情報を取得
    public GameObject Player;
    public GameObject Bullet;
    //プレイヤーの位置
    Vector2 PlayerPos;
    //弾の位置
    Vector3 offsetpos;
    //弾の速さ
    public float shotSpeed;
    
    enum DIR{
        RIGHT,
        LEFT
    };
    DIR dir;

    // Use this for initialization
    void Start () {
        EAtackFlag = false;
        EBulletFlag = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //ここで球を出す本体が見えているか探す
        if (GetComponent<Renderer>().isVisible)
        {
            EAtackFlag = true;
        }
        else 
        {
            EAtackFlag = false;
        }
        AI();
    }

    void AI()
    {
        //プレイヤーのいる方向を探す
        if (transform.position.x >= Player.transform.position.x)
        {
            dir = DIR.LEFT;
            offsetpos = -(transform.localScale / 2);
        }
        else
        {
            dir = DIR.RIGHT;
            offsetpos = (transform.localScale / 2);
        }

        if (EAtackFlag == true)
        {
            //弾が自分の弾かどうか判別
            if (EBulletFlag == false)
            {
                //自分の弾なら一旦新しいオブジェクトを作る
                GameObject bulletClone = Instantiate(Bullet, transform.position, transform.rotation);
                BulletFunc ActBeam = bulletClone.GetComponent<BulletFunc>();
                ActBeam.Fire(transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y + 1.0f));
                bulletClone.transform.parent = transform;
            }
            else
            {
                //違えば処理なし
            }
        }
    }
    //外から攻撃するフラグ変えるやつ
    public void Flag(bool flag)
    {
        EBulletFlag = flag;
    }
}
