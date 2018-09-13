using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukaMove : MonoBehaviour {

    public int time = 100;
    public string PathName = "Kari";

    // Use this for initialization
    void Start()
    {
        //iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
        iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    { 
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
