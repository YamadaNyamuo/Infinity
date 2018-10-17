using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 

public class SquareFunc : MonoBehaviour {
    // 速度
    public float SPEED;
    private float tmpSpeed;
    private float downSpeed;
    //プレイヤーのRigidbody2Dを使うところさん
    private Rigidbody2D rb2D;

    //方向
    public enum Dir {
        NON,
        RIGHT,
        LEFT,
        UP
        //DOWN
    };

    //プレイヤーの行動
    public enum Act {
        STAY,
        WALK,
        ATTACK,
        LOW,
        NON
    }

    //---------ジャンプ関連に使うもの-----------
    ////当たってる物のレイヤー
    //public LayerMask stage;
    ////ジャンプ時のセンサー
    //private Transform check;
    //private Transform endck;
    ////センサーに引っかかってるか調べるフラグ
    //public bool checkflag;
    //public bool endflag;
    ////飛ぶフラグ
    //private bool jumpflag;
    ////プレイヤーのジャンプ力ぅ…ですかね
    //private float jumpPow;
    ////もともとのジャンプ力
    //private float OriJumpPow;
    ////飛んでる間加算する時間
    private float jumpTimer;
    //--------------------------------------------

    //入力方向
    public Dir inputDir;
    //プレイヤーの向いてる向き
    public Dir drawDir;
    //前フレームで向いてる方向
    private Dir oldDir;
    //移動しているかどうかを返すフラグ
    public bool moveFlag;
    //プレイヤーの方向でステージが当たってるか判別
    private bool rightFlag;
    private bool leftFlag;
    //アニメーションの関数
    private new Animator animation ;
    //プレイヤーの当たり判定
    private Vector2 collSize;
    //入力方向
    private float direction;
    //重力加速度　プレイヤーの落下に使うよ
    private float a;
    //当たるかどうかのフラグ　無敵時間の調整などにどうぞ
    //デフォルトはtrueで当たるように使ってくり
    public bool HitFlag;

    //timeOut[ms]毎に処理を実行する
    public float timeOut;
    //時間がたつほど増える値
    private float timeElapsed;
    //立ってる時のオフセット値
    private Vector2 StandCollOff;

    void Start()
    {
        //SPEED = 0.05f;
        tmpSpeed        = SPEED;
        downSpeed       = SPEED * 0.55f;
        //check           = transform.Find("check1");
        //endck           = transform.Find("check2");
        rb2D            = GetComponent<Rigidbody2D>();
        //jumpflag        = false;
        moveFlag        = false;
        animation       = GetComponent<Animator>();
        //jumpPow         = 10.0f;
        //OriJumpPow      = jumpPow;
        jumpTimer       = 0.0f;
        collSize        = GetComponent<CapsuleCollider2D>().size;
        a               = 11.0f;
        HitFlag         = true;
        StandCollOff    = GetComponent<CapsuleCollider2D>().offset;
        inputDir     = Dir.NON;
        drawDir      = Dir.RIGHT;
    }

    void Update()
    {
        if (HitFlag == false)
        {
            timeElapsed += Time.deltaTime;
            //ダメージの点滅処理
            float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, level);
            if (timeElapsed >= timeOut)
            {
                HitFlag = true;
                timeElapsed = 0.0f;
                //透過を元に戻す
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1.0f);
            }
        }
        DirInput();
        Move();
        Jump();
    }

    //Rigidbody関連の処理はなるべくここに書く
    private void FixedUpdate()
    {
        //マイナス方向にかける負荷の値 摩擦力
        float F = 3.0f;
        //左右キーの入力で移動（Addforce）
        //実際の速さがSPEEDの値を超えないように速度制限つけてる
        if (rb2D.velocity.magnitude <= SPEED)
        {
            //滑るような感じを無くしたい
            rb2D.AddForce(Vector2.right * direction * SPEED - (rb2D.velocity * F));
        }
        else
        {
            //速度を超えていたらマイナス方向に速さ分加える
            rb2D.AddForce(-(rb2D.velocity * F));
        }
        ////フラグがtrueだったら飛ぶ処理
        //if (jumpflag == true)
        //{
        //    if (rb2D.velocity.magnitude <= SPEED)
        //    {
        //        rb2D.AddForce(new Vector2(0.0f, jumpPow), ForceMode2D.Impulse);
        //    }
        //}
        //ここ重力の計算
        //aが重力加速度　jumpTimerが飛んでる時間
        //あってるかわからないってか物理法則にあってないからあてにしないこと！
        //rb2D.AddForce(Vector2.down * a * 0.0f * rb2D.mass);
    }

    //衝突処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //当たってるオブジェクトのGameObjectの情報
        GameObject collObj = collision.gameObject;
        int tmpAnim = animation.GetInteger("ActID");
        //ステージと敵に当たりっぱなしの時重力加速度を1.0fにしとく
        if ((collObj.layer == 9) ||
            (collObj.layer == 11))
        {
            if ((tmpAnim != 5) &&
                (tmpAnim != 6) &&
                (tmpAnim != 7))
            {
                jumpTimer = 1.0f;
            }
        }
    }

    //入力処理用関数
    void DirInput ()
    {
        //入力されている時だけ
        //左右の入力を確認するところ
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            inputDir = Dir.RIGHT;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            inputDir = Dir.LEFT;
        }
        else
        {
            inputDir = Dir.NON;
        }

        //左右反転
        if ((inputDir == Dir.LEFT && drawDir == Dir.RIGHT)
         || (inputDir == Dir.RIGHT && drawDir == Dir.LEFT))
        {
            Vector3 rotate = transform.localScale;
            rotate.x *= -1;
            transform.localScale = rotate;
        }

    }



    //移動管理用関数
    void Move()
    {
        //移動処理
        Vector3 Position = transform.localPosition;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = -1;
        }
        else
        {
            direction = 0;
        }

        //動いてる時だけmoveフラグをtrueにする
        if ((Position.x - transform.localPosition.x) < SPEED)
        {
            moveFlag = true;
        }

        //描画方向を変える
        if (inputDir == Dir.NON)
        {
            oldDir = drawDir;
            drawDir = oldDir;
            moveFlag = false;
        }
        else
        {
            drawDir = inputDir;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") == true)
        {
            rb2D.AddForce(Vector2.up * 50.0f,ForceMode2D.Impulse);
        }
    }
}
