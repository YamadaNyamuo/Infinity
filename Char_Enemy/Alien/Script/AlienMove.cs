using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AlienMove : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 7f;
    public Animator animator;
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
    public enum ALIEN_STATE
    {
        STOP, //停止
        MOVE, //移動(徘徊)
        CHASE,//追跡
    }

    public ALIEN_STATE state;

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
        state = ALIEN_STATE.MOVE;
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
            case ALIEN_STATE.STOP:
                animator.SetBool("Move", false);
                if (timeElapsed >= timeOut)
                {
                    float angle = (hori >= 0 ? Mathf.LerpAngle(minAngle, maxAngle, Time.time) : Mathf.LerpAngle(maxAngle, minAngle, Time.time));
                    transform.eulerAngles = new Vector3(0, angle, 0);
                    timeElapsed = 0;
                    state = ALIEN_STATE.MOVE;
                }
                break;
            case ALIEN_STATE.MOVE:
                animator.SetBool("Move", true);
                horizontalInput = hori;
                if (timeElapsed >= timeOut)
                {
                    hori *= (-1.0f);
                    timeElapsed = 0;
                    state = ALIEN_STATE.STOP;
                    horizontalInput = 0;
                }
                break;
            case ALIEN_STATE.CHASE:
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
        }
    }

    public ALIEN_STATE GetState()
    {
        return state;
    }

    public void SetState(ALIEN_STATE mode, Transform obj = null)
    {
        playerTransform = obj;
        state = mode;
    }
}
