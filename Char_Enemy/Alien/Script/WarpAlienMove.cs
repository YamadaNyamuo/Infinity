using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WarpAlienMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 7f;
    public Animator animator;
    public bool warpFlag; // ワープ判定
    public float WS;

    public float timeOut;
    public float timeElapsed;

    float horizontalInput;
    private float hori; // 向き

    float minAngle = 270.0F; // 左
    float maxAngle = 90.0F;  // 右

    public Transform playerTransform;
    public Transform plTransform;
    public GameObject player;

    PlayerMove plMove;
    GameObject footArea;
    AlienGroundCheck groundCheck;

    private float g = (-9.81f); // 重力
    public enum WARP_ALIEN_STATE
    {
        STOP, //停止
        MOVE, //移動(徘徊)
        CHASE,//追跡
        WARP  //ワープ
    }

    public WARP_ALIEN_STATE state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        footArea = this.transform.Find("FootArea").gameObject; // 足元の判定
        groundCheck = footArea.GetComponent<AlienGroundCheck>();
        player = GameObject.Find("Player");
        plTransform = player.transform;
        plMove = player.GetComponent<PlayerMove>();
        timeElapsed = 0; 
        hori = (-1.0f);
        warpFlag = false;
        state = WARP_ALIEN_STATE.MOVE;
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        WS = horizontalInput * speed;

        if (groundCheck.ground == false)
        {
            rb.AddForce(new Vector3(speed * (WS - rb.velocity.x), Vector2.up.y + g, 0));
        }
        else
        {
            rb.AddForce(new Vector3(speed * (WS - rb.velocity.x), 0, 0));
        }
          
        //Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, -1, 0), 100);

        //// 可視化
        //Debug.DrawRay(pos, new Vector3(0, -100, 0), Color.blue, 1);
        //Debug.Log(hit.collider);

        //if(hit.collider != footArea && hit.collider == null)
        //{
        //    speed = 0;
        //}
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        switch (state)
        {
            case WARP_ALIEN_STATE.STOP:
                animator.SetBool("Move", false);
                if (timeElapsed >= timeOut)
                {
                    float angle = (hori >= 0 ? Mathf.LerpAngle(minAngle, maxAngle, Time.time) : Mathf.LerpAngle(maxAngle, minAngle, Time.time));
                    transform.eulerAngles = new Vector3(0, angle, 0);
                    timeElapsed = 0;
                    state = WARP_ALIEN_STATE.MOVE;
                }
                    break;
            case WARP_ALIEN_STATE.MOVE:
                animator.SetBool("Move", true);
                horizontalInput = hori;
                if (timeElapsed >= timeOut)
                {
                    hori *= (-1.0f);
                    timeElapsed = 0;
                    state = WARP_ALIEN_STATE.STOP;
                    horizontalInput = 0;
                }
                break;
            case WARP_ALIEN_STATE.CHASE:
                animator.SetBool("Move", true);
                // プレイヤーの方向を向かせる
                if (playerTransform.position.x > transform.position.x)
                {
                    hori = 1.0f;
                }
                else if (playerTransform.position.x < transform.position.x)
                {
                    hori = (-1.0f);
                }
                else;
                float angle2 = (hori >= 0 ? Mathf.LerpAngle(minAngle, maxAngle, Time.time) : Mathf.LerpAngle(maxAngle, minAngle, Time.time));
                transform.eulerAngles = new Vector3(0, angle2, 0);
                horizontalInput = hori;
                break;
            case WARP_ALIEN_STATE.WARP:
                // ワープ中動かさない
                if (warpFlag == false)
                {
                    rb.bodyType = RigidbodyType2D.Static;
                }
                animator.SetBool("Move", false);
                horizontalInput = 0;
                if (timeElapsed >= timeOut)
                {
                    // プレイヤーが左を向いていたら左に、インド人が右を向いていたら右にワープする
                    if (warpFlag == false && plMove.GetPlayerDir() == PlayerMove.Dir.RIGHT)
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                        groundCheck.ground = false;
                        warpFlag = true;
                        hori = (-1.0f);
                        float angle3 = (hori >= 0 ? Mathf.LerpAngle(minAngle, maxAngle, Time.time) : Mathf.LerpAngle(maxAngle, minAngle, Time.time));
                        transform.eulerAngles = new Vector3(0, angle3, 0);
                        transform.position = new Vector3(plTransform.position.x + 5, plTransform.position.y + 7, plTransform.position.z);
                    }
                    else if (warpFlag == false && plMove.GetPlayerDir() == PlayerMove.Dir.LEFT)
                    {
                        rb.bodyType = RigidbodyType2D.Dynamic;
                        groundCheck.ground = false;
                        warpFlag = true;
                        hori = 1.0f;
                        float angle3 = (hori >= 0 ? Mathf.LerpAngle(minAngle, maxAngle, Time.time) : Mathf.LerpAngle(maxAngle, minAngle, Time.time));
                        transform.eulerAngles = new Vector3(0, angle3, 0);
                        transform.position = new Vector3(plTransform.position.x - 5, plTransform.position.y + 7, plTransform.position.z);
                    }
                }

                // ワープして地面に着いたら停止状態に一旦遷移させる
                if(warpFlag == true && groundCheck.ground == true)
                {
                    warpFlag = false;
                    timeElapsed = 0;
                    state = WARP_ALIEN_STATE.STOP;
                    horizontalInput = 0;
                }
                break;
        }
    }

    public WARP_ALIEN_STATE GetState()
    {
        return state;
    }

    public void SetState(WARP_ALIEN_STATE mode, Transform obj = null)
    {
        playerTransform = obj;
        state = mode;
    }
}
