using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGroundCheck : MonoBehaviour
{

    public bool ground;
    BoxCollider2D col;

    // Use this for initialization
    void Start()
    {
        ground = false;
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            ground = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            ground = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            ground = false;
        }
    }
}

