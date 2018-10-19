using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    public bool ground;
    GameObject parent;
    public Animator animator;
    AnimatorStateInfo animStateInfo;

    // Use this for initialization
    void Start () {
        ground = false;
        parent = transform.root.gameObject;
        animator = parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            ground = true;
            animator.SetBool("Ground", true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            ground = true;
            animator.SetBool("Jumping", false);
            animator.SetBool("Ground", true);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            ground = false;
            animator.SetBool("Jumping", false);
            animator.SetBool("Ground", false);
        }
    }
}
