using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloorDestroy : MonoBehaviour {

    Rigidbody2D rb;

    GameObject parent;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        parent = transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(parent.gameObject);
        }
    }
}
