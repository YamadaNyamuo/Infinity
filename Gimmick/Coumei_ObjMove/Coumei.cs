using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Coumei : MonoBehaviour {

    private SpriteRenderer sprite;
    private TilemapRenderer tilemap;
    private Color defColor;

    // Use this for initialization
    void Start () {
        if(transform.GetComponent<Tilemap>()!=null)
        {
            tilemap = transform.GetComponent<TilemapRenderer>();
            tilemap.enabled = false;
        }
        else
        {
            sprite = GetComponent<SpriteRenderer>();
            defColor = sprite.color;
            sprite.color = new Color(0f, 0f, 0f, 0f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (transform.GetComponent<Tilemap>() != null)
            {
                tilemap.enabled = true;
            }
            else
            {
            sprite.color = defColor;
            }
        }
    }
}
