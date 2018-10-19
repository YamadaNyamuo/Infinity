using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMave : MonoBehaviour
{
    public enum EnemyState
    {
        Walk,
        Wait,
        Chase
    };

    //　敵の状態
    private EnemyState state;
    //　追いかけるキャラクター
    private Transform playerTransform;

    Rigidbody _rb;
    public bool isUseCameraDirection;    // カメラの向きに合わせて移動させたい場合はtrue
    public float moveSpeed;              // 移動速度
    public float moveForceMultiplier;    // 移動速度の入力に対する追従度
    public GameObject mainCamera;
    float _horizontalInput;
    float _verticalInput;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        state = EnemyState.Walk;
    }

    // Update is called once per frame
    void Update()
    {
        //_horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        //　キャラクターを追いかける状態であればキャラクターの目的地を再設定
        if (state == EnemyState.Chase)
        {
            if(playerTransform.position.x < transform.position.x)
            {
                _horizontalInput=(_horizontalInput < -1.0f ?  -1.0f : _horizontalInput -=(0.1f));
            }
        }
    }

    public void SetState(string mode, Transform obj = null)
    {
        if (mode == "walk")
        {
            //arrived = false;
            //elapsedTime = 0f;
            //state = EnemyState.Walk;
            //setPosition.CreateRandomPosition();
        }
        else if (mode == "chase")
        {
            state = EnemyState.Chase;
            //　追いかける対象をセット
            playerTransform = obj;
        }
        else if (mode == "wait")
        {
 
        }
    }

    void FixedUpdate()
    {
        Vector3 moveVector = Vector3.zero;    // 移動速度の入力

        if (isUseCameraDirection)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            Vector3 cameraRight = mainCamera.transform.right;
            cameraForward.y = 0.0f;    // 水平方向に移動させたい場合はy方向成分を0にする
            cameraRight.y = 0.0f;

            moveVector = moveSpeed * (cameraRight.normalized * _horizontalInput + cameraForward.normalized * _verticalInput);
        }
        else
        {
            moveVector.x = moveSpeed * _horizontalInput;
            moveVector.z = moveSpeed * _verticalInput;
        }

        _rb.AddForce(moveForceMultiplier * (moveVector - _rb.velocity));
    }

    public EnemyState GetState()
    {
        return state;
    }
}