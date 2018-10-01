using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coumei : MonoBehaviour {

    private SpriteRenderer sprite;

    private Color defColor;

    // Use this for initialization
    void Start () {
        sprite = GetComponent<SpriteRenderer>();
        defColor = sprite.color;
        sprite.color=new Color(0f,0f,0f,0f);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            sprite.color = defColor;
        }
    }
}
