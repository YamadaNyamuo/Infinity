using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public enum ENEMY_STETA
    {
        STOP,//停止
        MOVE,//移動(徘徊)
        CHASE//追跡
    }

    Rigidbody2D _rb;
    public bool isUseCameraDirection;    // カメラの向きに合わせて移動させたい場合はtrue
    public float moveSpeed;              // 移動速度
    public float moveForceMultiplier;    // 移動速度の入力に対する追従度
    public GameObject mainCamera;
    float _horizontalInput;
    float _verticalInput;

    public Transform playerTransform;

    public float timeOut;
    private float timeElapsed;

    private float hori;

    float minAngle = 0.0F;
    float maxAngle = 180.0F;

    public ENEMY_STETA steta;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        hori = (-1.0f);
        steta = ENEMY_STETA.MOVE;
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime;
        switch(steta)
        {
            case ENEMY_STETA.STOP:
                if (timeElapsed >= timeOut)
                {
                    float angle = (hori >= 0 ? Mathf.LerpAngle(minAngle, maxAngle, Time.time) : Mathf.LerpAngle(maxAngle, minAngle, Time.time));
                    transform.eulerAngles = new Vector3(0, angle, 0);
                    timeElapsed = 0;
                    steta = ENEMY_STETA.MOVE;
                }

                break;
            case ENEMY_STETA.MOVE:
                _horizontalInput = hori;
                if (timeElapsed >= timeOut)
                {
                    hori *= (-1.0f);
                    timeElapsed = 0;
                    steta = ENEMY_STETA.STOP;
                    _horizontalInput = 0;
                }
                break;
            case ENEMY_STETA.CHASE:
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
                _horizontalInput = hori;
                break;
        }

        //_verticalInput = Input.GetAxis("Vertical");

    }
    void FixedUpdate()
    {
        Vector2 moveVector = Vector2.zero;    // 移動速度の入力

        if (isUseCameraDirection)
        {
            Vector2 cameraForward = mainCamera.transform.forward;
            Vector2 cameraRight = mainCamera.transform.right;
            cameraForward.y = 0.0f;    // 水平方向に移動させたい場合はy方向成分を0にする
            cameraRight.y = 0.0f;

            moveVector = moveSpeed * (cameraRight.normalized * _horizontalInput + cameraForward.normalized * _verticalInput);
        }
        else
        {
            moveVector.x = moveSpeed * _horizontalInput;
            //moveVector.z = moveSpeed * _verticalInput;
        }

        _rb.AddForce(moveForceMultiplier * (moveVector - _rb.velocity));
    }

    public ENEMY_STETA GetState()
    {
        return steta;
    }

    public void SetState(ENEMY_STETA mode, Transform obj = null)
    {
        playerTransform = obj;
        steta = mode;
    }
}
