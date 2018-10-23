using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb, targetRb;
    private float speed = 8f;
    private float jump = 300;
    private Animator animator;
    AnimatorStateInfo animStateInfo;
    bool ground;
    private float WS;

    GameObject footArea;
    public GameObject target, target2;
    GroundCheck groundCheck;

    bool jumpBotton;
    float flameCount;

    private float oldJump = 0;
    private float g = (-9.81f);
    private int moveFlag = 0;//左右への移動できるかどうか　0:移動可能　1:右移動不可　2:左移動不可　3:左右移動不可

    private RaycastHit2D[] l_Hit= { new RaycastHit2D(), new RaycastHit2D(), new RaycastHit2D() };
    private RaycastHit2D d_Hit;
    private RaycastHit2D[] r_Hit = { new RaycastHit2D(), new RaycastHit2D(), new RaycastHit2D() };
    Vector3 tmpPos;
    private Vector2[] rayDir;
    private Ray[] ray;

    //public GameObject target, target2;
    public CheckPointData checkPointData;
    private Vector3 startPosition;
    float fadeSpeed = 0.02f;
    float red, green, blue, alfa;

    public bool isFadeOut = false;
    public bool isFadeIn = false;

    private bool DeathFlag;

    public enum Dir
    {
        LEFT,  // 左
        RIGHT, // 右
        NON    // 方向なっしー！！！ヒャッハーーー！！！！
    }

    //入力方向
    private Dir inputDir;
    //プレイヤーの向いてる向き
    private Dir drawDir;
    //前フレームで向いてる方向
    private Dir oldDir;

    private LoadSceneManager loadSceneManager;

    //Use this for initialization
    void Start()
    {
        inputDir = Dir.NON;
        drawDir = Dir.RIGHT;
        rayDir = new Vector2[] { Vector2.right , Vector2.down , Vector2.left };
        rb = GetComponent<Rigidbody2D>();
        footArea = this.transform.Find("FootArea").gameObject; // 足元の判定
        groundCheck = footArea.GetComponent<GroundCheck>();
        animator = GetComponent<Animator>();
        //Input.GetAxis("Vertical") * speed;
        
        //targetRb = target.GetComponent<Rigidbody2D>();
        //target.transform.position = aaa.startPosition;
        transform.position = checkPointData.startPosition;
        //startPosition =
        DeathFlag = false;
        loadSceneManager = GameObject.Find("Management").GetComponent<LoadSceneManager>();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        //--空中にいるときに壁にくっつかないようにする処理
        moveFlag = 0;
        tmpPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);//Rayに使うプレイヤーの中心の座標

        //上以外の三方向に飛ばすRayを作る(左右は三つに分ける)
        ray = new Ray[]{
            new Ray(new Vector3 (tmpPos.x,tmpPos.y+0.7f,tmpPos.z), rayDir[0]),  
            new Ray(tmpPos, rayDir[0]),                                         
            new Ray(new Vector3 (tmpPos.x,tmpPos.y-0.7f,tmpPos.z), rayDir[0]),  
            new Ray(tmpPos, rayDir[1]),                                         
            new Ray(new Vector3 (tmpPos.x,tmpPos.y+0.7f,tmpPos.z), rayDir[2]),  
            new Ray(tmpPos, rayDir[2]),                                         
            new Ray(new Vector3 (tmpPos.x,tmpPos.y-0.7f,tmpPos.z), rayDir[2]) };

        r_Hit[0] = Physics2D.Raycast(ray[0].origin, ray[0].direction, 0.7f);    //右のRayその1(大体目のあたり)
        r_Hit[1] = Physics2D.Raycast(ray[1].origin, ray[1].direction, 0.7f);    //右のRayその2(体の真ん中から右)
        r_Hit[2] = Physics2D.Raycast(ray[2].origin, ray[2].direction, 0.7f);    //右のRayその3(大体太もものあたり)
        d_Hit = Physics2D.Raycast(ray[3].origin, ray[3].direction, 1.1f);       //下のRay(真ん中から下)
        l_Hit[0] = Physics2D.Raycast(ray[4].origin, ray[4].direction, 0.7f);    //左のRayその1(大体目のあたり)
        l_Hit[1] = Physics2D.Raycast(ray[5].origin, ray[5].direction, 0.7f);    //左のRayその2(体の真ん中から右)
        l_Hit[2] = Physics2D.Raycast(ray[6].origin, ray[6].direction, 0.7f);    //左のRayその3(大体太もものあたり)
        
        //下に飛ばしているRayが当たってない時(空中にいる場合)
        if (d_Hit.collider == null)
        {
            //三つのうちどれかでも壁を探知すれば右に移動するのを禁止
            if (r_Hit[0].collider != null || r_Hit[1].collider != null || r_Hit[2].collider != null)
            {
                moveFlag += 1;
            }
            //三つのうちどれかでも壁を探知すれば左に移動するのを禁止
            if (l_Hit[0].collider != null || l_Hit[1].collider != null || l_Hit[2].collider != null)
            {
                moveFlag += 2;
            }
        }

        //左右どちらにも移動できない場合以外のみこの処理の中に入る
        if (moveFlag != 3)
        {
            //右移動だけできなくする
            if ((Input.GetAxis("Horizontal") > 0) && (moveFlag == 1))
            {
                WS = (Input.GetAxis("Horizontal") > 0 ? 0 : Input.GetAxis("Horizontal")) * speed;
            }
            //左移動だけできなくする
            else if ((Input.GetAxis("Horizontal") < 0) && (moveFlag == 2))
            {
                WS = (Input.GetAxis("Horizontal") < 0 ? 0 : Input.GetAxis("Horizontal")) * speed;
            }
            //移動制限がないとき
            else
            {
                WS = Input.GetAxis("Horizontal") * speed;
            }
        }

        //--左右どちらに向いているか
        //右に向いている場合
        if (Input.GetAxis("Horizontal") > 0)
        {
            inputDir = Dir.RIGHT;
        }
        //左に向いている場合
        else if (Input.GetAxis("Horizontal") < 0)
        {
            inputDir = Dir.LEFT;
        }
        else
        {
            inputDir = Dir.NON;
        }

        if (inputDir == Dir.NON)
        {
            oldDir = drawDir;
            drawDir = oldDir;
        }
        else
        {
            drawDir = inputDir;
        }

        animStateInfo = animator.GetCurrentAnimatorStateInfo(0); // animatorのStateを取得

        //右移動
        if (inputDir == Dir.RIGHT)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            //アニメーション
            animator.SetBool("Running", true);
        }
        //左移動
        else if (inputDir == Dir.LEFT)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Running", true);
        }

        //何もキーを押していない時はアニメーションをオフにする
        else
        {
            animator.SetBool("Running", false);
        }

        //--ジャンプ処理
        //ジャンプボタン(スペースキー)が押されていない場合
        if (jumpBotton == false)
        {
            //スペースキーでジャンプする
            if (Input.GetButton("Jump"))
            {
                animator.SetBool("Jumping", true);
                rb.AddForce(new Vector3(0, (jump - oldJump) * Vector2.up.y, 0));
                //入力(ボタンを押されてる)されてる時間
                flameCount = flameCount + 1 * Time.deltaTime;
                oldJump = jump - (jump / 10);
                //ジャンプアニメーション中に地面に着いたらもう一度再生する
                //if (animStateInfo.fullPathHash == Animator.StringToHash("Base Layer.Jump"))
                //{
                //    animator.Play("Base Layer.Jump", 0, 0.0f);
                //}
                //else if (animStateInfo.fullPathHash != Animator.StringToHash("Base Layer.Jump"))
                //{
                //    animator.Play("Base Layer.Jump", 0, 0.0f);
                //}
                //Input.GetAxis("jump");
            }

            //ボタンを離すか、ジャンプ入力が最大までされた場合、ボタンを押したよってフラグをtrueにしておく
            if (Input.GetButtonUp("Jump"))
            {
                jumpBotton = true;
            }
            else if (flameCount >= 0.30f)
            {
                jumpBotton = true;
            }
            else { }

        }

        //上方向に向けて力を加える
        //rb.AddForce(new Vector3(0, jump*Vector2.up.y, 0));
        //    ground = false;
        //    animator.SetBool("Jumping", true);

        //    // ジャンプアニメーション中に地面に着いたらもう一度再生する
        //    if (animStateInfo.fullPathHash == Animator.StringToHash("Base Layer.Jump"))
        //    {
        //        animator.Play("Base Layer.Jump", 0, 0.0f);
        //    }
        //    else if (animStateInfo.fullPathHash != Animator.StringToHash("Base Layer.Jump"))
        //    {
        //        animator.Play("Base Layer.Jump", 0, 0.0f);
        //    }
        //}
        //else
        //{
        //    animator.SetBool("Jumping", false);
        //}
        // }

        //地面についているかどうかによって処理を変える
        if (groundCheck.ground == false)
        {
            //ついていない場合は下に落ちる処理も加える
            rb.AddForce(new Vector3(speed * (WS - rb.velocity.x), Vector2.up.y + g, 0));
        }
        else
        {
            rb.AddForce(new Vector3(speed * (WS - rb.velocity.x), 0, 0));
        }


        //--デバック用にRayを見えるようにする
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);

        // 可視化
        Debug.DrawRay(pos, new Vector3(0, 100, 0), Color.blue, 1);

        for (int i = 0; i < ray.Length; i++)
        {
            Debug.DrawRay(ray[i].origin, ray[i].direction, Color.red, 0.1f);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //地面についたらジャンプできるようにするよ
        if (groundCheck.ground == true)
        {
            animator.SetBool("Jumping", false);
            if (Input.GetButton("Jump") == false)
            {
                jumpBotton = false;
                flameCount = 0;
                oldJump = 0f;
            }
        }
        if ((collision.gameObject.tag == "Death") || (collision.gameObject.tag == "Enemy"))
        {
            if(DeathFlag==false)
            {
                loadSceneManager.FadeAndLoadScene("DefaulStageScenes");
                DeathFlag = true;
            }
        }
    }

    public Dir GetPlayerDir()
    {
        return drawDir;
    }
}