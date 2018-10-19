using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMove : MonoBehaviour
{

    public enum DIR
    {
        DIR_NON,
        DIR_UP,
        DIR_RIGHT,
        DIR_DOWN,
        DIR_LEFT
    }

    [TooltipAttribute("動くオブジェクトの動かし方の切り替え\n trueにするとほかのオブジェクトとも干渉するようになる")]
    public bool moveMode=false;

    public DIR MoveDir;

    public float MoveSpeed;

    private bool MoveFlag = false;

    private Rigidbody2D rig;

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveFlag == true)
        {
            if (moveMode)
            {
                switch (MoveDir)
                {
                    case DIR.DIR_UP:
                        rig.AddForce(new Vector2(0,  MoveSpeed));
                        break;
                    case DIR.DIR_RIGHT:
                        rig.AddForce(new Vector2(MoveSpeed, 0));
                        break;
                    case DIR.DIR_DOWN:
                        rig.AddForce(new Vector2(0,  MoveSpeed));
                        break;
                    case DIR.DIR_LEFT:
                        rig.AddForce(new Vector2( MoveSpeed, 0));
                        break;
                }
            }
            else
            {
                switch (MoveDir)
                {
                    case DIR.DIR_UP:
                        transform.position = new Vector3(transform.position.x, transform.position.y + MoveSpeed, transform.position.z);
                        break;
                    case DIR.DIR_RIGHT:
                        transform.position = new Vector3(transform.position.x + MoveSpeed, transform.position.y, transform.position.z);
                        break;
                    case DIR.DIR_DOWN:
                        transform.position = new Vector3(transform.position.x, transform.position.y - MoveSpeed, transform.position.z);
                        break;
                    case DIR.DIR_LEFT:
                        transform.position = new Vector3(transform.position.x - MoveSpeed, transform.position.y, transform.position.z);
                        break;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            MoveFlag = true;
        }
    }
}
