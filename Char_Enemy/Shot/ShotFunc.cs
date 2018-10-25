using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFunc : MonoBehaviour {

    //敵の攻撃フラグ
    public bool EAtackFlag;

    //プレイヤーの情報を取得
    public GameObject Player;
    public GameObject Bullet;

    Vector2 PlayerPos;
    Vector3 offsetpos;

    enum DIR{
        RIGHT,
        LEFT
    };
    DIR dir;

    // Use this for initialization
    void Start () {
        EAtackFlag = false;
        //Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GetComponent<Renderer>().isVisible)
        {
            AI();
            if (!GameObject.Find("Bullet(Clone)"))
            {
                EAtackFlag = true;
            }
        }
        else
        {
            EAtackFlag = false;
        }
        
    }

    void AI()
    {
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
            //一旦新しいオブジェクトを作る
            GameObject bulletClone = Instantiate(Bullet, transform.position, transform.rotation);
            BulletFunc ActBeam = bulletClone.GetComponent<BulletFunc>();
            ActBeam.Fire(transform.position ,Player.transform.position);
            EAtackFlag = false;
        }
    }
}
