using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;
    public float jump = 400;
    public Animator animator;
    AnimatorStateInfo animStateInfo;
    bool ground;
    public float WS;

    GameObject footArea;
    GroundCheck groundCheck;

    bool jumpBotton;
    float flameCount;

    private float oldJump = 0;
    private float g = (-9.81f);
    private int moveFlag = 0;//0:移動可能　1:右移動不可　2:左移動不可　3:左右移動不可

    RaycastHit2D l_Hit;
    RaycastHit2D d_Hit;
    RaycastHit2D r_Hit;
    Vector2 vec = new Vector2(0, 1);
    Vector3 tmpPos;
    private Vector2[] rayDir;
    private Ray[] ray;

    //Use this for initialization

    void Start()
    {
        rayDir = new Vector2[] { Vector2.up, Vector2.right , Vector2.down , Vector2.left };
        rb = GetComponent<Rigidbody2D>();
        footArea = this.transform.Find("FootArea").gameObject; // 足元の判定
        groundCheck = footArea.GetComponent<GroundCheck>();
        animator = GetComponent<Animator>();
        //Input.GetAxis("Vertical") * speed;

    }

    //Update is called once per frame
    void FixedUpdate()
    {

        moveFlag = 0;
        tmpPos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        ray = new Ray[]{
            new Ray(tmpPos, rayDir[0]),
            new Ray(tmpPos, rayDir[1]),
            new Ray(tmpPos, rayDir[2]),
            new Ray(tmpPos, rayDir[3]) };
        
        r_Hit = Physics2D.Raycast(ray[1].origin, ray[1].direction, 0.7f);
        d_Hit = Physics2D.Raycast(ray[2].origin, ray[2].direction, 1.1f);
        l_Hit = Physics2D.Raycast(ray[3].origin, ray[3].direction, 0.7f);
        //Debug.Log(_hit.collider.gameObject.tag);
        if (d_Hit.collider == null)
        {
            //右に移動するのを禁止
            if (r_Hit.collider != null)
            {
                moveFlag += 1;
            }
            //左に移動するのを禁止
            if (l_Hit.collider != null)
            {
                moveFlag += 2;
            }
        }

        if(moveFlag!=3)
        {
            if((Input.GetAxis("Horizontal") > 0) && (moveFlag == 1))
            {
                WS = (Input.GetAxis("Horizontal") > 0 ? 0 : Input.GetAxis("Horizontal")) * speed;
            }
            else if ((Input.GetAxis("Horizontal") < 0) && (moveFlag == 2))
            {
                WS = (Input.GetAxis("Horizontal") < 0 ? 0 : Input.GetAxis("Horizontal")) * speed;
            }
            else
            {
                WS = Input.GetAxis("Horizontal") * speed;
            }
        }
        
        animStateInfo = animator.GetCurrentAnimatorStateInfo(0); // animatorのStateを取得

        //右移動
        if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.Keypad6)))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            //アニメーション
            animator.SetBool("Running", true);
        }
        //左移動
        else if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.Keypad4)))
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            animator.SetBool("Running", true);
        }

        //何もキーを押していない時はアニメーションをオフにする
        else if ((Input.GetKey(KeyCode.RightArrow) == false) && (Input.GetKey(KeyCode.LeftArrow) == false))
        {
            animator.SetBool("Running", false);
        }


        if (jumpBotton == false)
        {
            //スペースキーでジャンプする
            if (Input.GetButton("Jump"))
            {
                animator.SetBool("Jumping", true);
                rb.AddForce(new Vector3(0, (jump - oldJump) * Vector2.up.y, 0));
                flameCount = flameCount + 1 * Time.deltaTime;
                oldJump = jump - (jump / 10);
                //Input.GetAxis("jump");
            }
            if (flameCount >= 0.12f)
            {
                jumpBotton = true;
            }
            else if (Input.GetButtonUp("Jump"))
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


        if (groundCheck.ground == false)
        {
            rb.AddForce(new Vector3(speed * (WS - rb.velocity.x), Vector2.up.y + g, 0));
        }
        else
        {
            rb.AddForce(new Vector3(speed * (WS - rb.velocity.x), 0, 0));
        }

        Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);

        // 可視化
        //Debug.DrawRay(pos, new Vector3(0, 100, 0), Color.blue, 1);

        for(int i=0;i<ray.Length;i++)
        {
            Debug.DrawRay(ray[i].origin, ray[i].direction, Color.red, 0.1f);

        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.transform.tag == "Ground")
        if(groundCheck.ground )
        {
            animator.SetBool("Jumping", false);
            if (Input.GetButton("Jump") == false)
            {
                jumpBotton = false;
                flameCount = 0;
                oldJump = 0f;
            }
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if(collision.transform.tag=="Ground")
    //    {
    //        r_Hit = Physics2D.Raycast(ray[1].origin, ray[1].direction, 0.7f);
    //        d_Hit = Physics2D.Raycast(ray[2].origin, ray[2].direction, 1.1f);
    //        l_Hit = Physics2D.Raycast(ray[3].origin, ray[3].direction, 0.7f);
    //        //Debug.Log(_hit.collider.gameObject.tag);
    //        if (d_Hit.collider==null)
    //        {
    //            if (r_Hit.collider != null)
    //            {
    //                Debug.Log("インド人を右");
    //            }
    //            if (l_Hit.collider != null)
    //            {
    //                Debug.Log("インド人を左");
    //            }
    //        }
    //    }
    //}
}



