using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusMove : MonoBehaviour
{

    //public Vector2 speed = new Vector2(0.05f, 0.05f);
    public float speed = 1; // サボテンくんの落下速度
    float upTimer;          // 元の位置に上がるまでの待機時間

    Animator animator;

    Vector2 position;       // 現在位置
    Vector2 startPosition;  // 初期位置

    Rigidbody2D rb;

    bool isGround = false;  // 地面に着いているかどうかのフラグ

    GameObject searchArea;

    CactusPLSearch plSearch;

    // サボテンくんの状態
    enum CactusState
    {
        Idle,
        Fall,
        Up
    }

    CactusState cactusState;

    // Use this for initialization
    void Start()
    {
        upTimer = 0;
        startPosition = transform.position;
        searchArea = transform.FindChild("SearchArea").gameObject;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        plSearch = searchArea.GetComponent<CactusPLSearch>();
        SetState("idle");
    }

    // Update is called once per frame
    void Update()
    {
        // 移動処理
        Move();
    }

    // 移動関数
    void Move()
    {
        // 現在位置をPositionに代入
        position = transform.position;

        // 地面に着いていない時
        if (isGround == false)
        {
            // プレイヤーが範囲内に来た時
            if (plSearch.plSearchFlag == true)
            {
                SetState("fall");
            }
        }

        // 地面に着いている時
        if (isGround == true)
        {
            upTimer += Time.deltaTime;

            // 3秒経ったら初期位置に向かって上がる
            if (upTimer >= 3.0f)
            {
                SetState("up");
            }
        }

        // 現在の位置に加算減算を行ったPositionを代入する
        transform.position = position;
    }

    public void SetState(string mode)
    {
        if (mode == "idle")     // Idle状態の時
        {
            cactusState = CactusState.Idle;
            upTimer = 0;
            plSearch.plSearchFlag = false;
            isGround = false;
            animator.SetBool("Up", false);
            animator.SetBool("Fall", false);
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (mode == "fall") // Fall状態の時
        {
            cactusState = CactusState.Fall;
            Fall();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (mode == "up")   // Up状態の時
        {
            cactusState = CactusState.Up;
            Up();
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    // プレイヤーが範囲内に来たら落下する
    void Fall()
    {
        position.y -= speed * Time.deltaTime;
        cactusState = CactusState.Fall;
        animator.SetBool("Fall", true);
    }

    // 地面に着いた状態で一定時間経ったら元の位置に上がる
    void Up()
    {
        if (position.y <= startPosition.y)
        {
            position.y += (speed / 3) * Time.deltaTime;
            cactusState = CactusState.Up;
            animator.SetBool("Up", true);
            animator.SetBool("Fall", false);
        }
        else
        {
            // 初期位置に来たらIdle状態になる
            SetState("idle"); 
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (isGround == false)
            {
                //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                rb.bodyType = RigidbodyType2D.Dynamic;
                Invoke("Kinematic", 0.1f);
                isGround = true;
            }
        }
    }

    void Kinematic()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}