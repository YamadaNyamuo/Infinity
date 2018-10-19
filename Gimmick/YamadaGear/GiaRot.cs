using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiaRot : MonoBehaviour
{

    // Use this for initialization

    private Rigidbody2D rb;
    [TooltipAttribute("回転のスピード")]
    public float rotSpeed=50.0f;
    [TooltipAttribute("Rigidbody2Dで回す場合スピードの値を大きくしてね")]
    public bool rotRigOnOff = false;

    void Start()
    {
        if(rotRigOnOff==true)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }


    // Update is called once per frame
    void Update()
    {
        
        if (rotRigOnOff)
        {
            rb.angularVelocity = rotSpeed;
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, rotSpeed));
        }
    }
}
