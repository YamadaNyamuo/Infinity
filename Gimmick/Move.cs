using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    Rigidbody2D _rb;
    public bool isUseCameraDirection;    // カメラの向きに合わせて移動させたい場合はtrue
    public float moveSpeed;              // 移動速度
    public float moveForceMultiplier;    // 移動速度の入力に対する追従度
    public GameObject mainCamera;
    float _horizontalInput;
    float _verticalInput;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

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
}
