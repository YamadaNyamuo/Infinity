using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSE : MonoBehaviour {

    public AudioClip jumpSound;

    private AudioSource audioSource;

    private Animator animator;

    GameObject parent;

    PlayerMove playerMove;

    // Use this for initialization
    void Start ()
    {
        parent = transform.parent.gameObject;

        playerMove = parent.GetComponent<PlayerMove>();

        animator = parent.GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Ground") && animator.GetBool("FallFlag") == false)
        {
            if (Input.GetButton("Jump"))
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }
}
