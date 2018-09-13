using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukaMove : MonoBehaviour {

    [TooltipAttribute("Endにつくまでにかかる時間")]
    public int time = 100;
    [TooltipAttribute("使いたいPathの名前")]
    public string PathName = "Kari";
    [TooltipAttribute("再生タイプの設定\n true:ループ再生するよ\n false:ループ再生しない")]
    public bool type;

    private string typeString;

    [TooltipAttribute("LoopTypeの種類\n Noad:ループしない\n Loop:ループ再生する\n PingPong:再生し終わると逆に再生するループ")]
    public iTween.LoopType loopType;
    [TooltipAttribute("EaseTypeの種類\n 動き方を変えられる\n 移動床ならLinearがいいかもしれない\n 多いから自分で調べろ！")]
    public iTween.EaseType easeType;

    // Use this for initialization
    void Start()
    {
        if (type)
        {
            typeString = "looptype";
            iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", easeType, typeString, loopType));
        }
        else
        {
            typeString = "easeType";
            iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", easeType));
        }
        //iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
        //iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", iTween.EaseType.linear, typeString, iTween.LoopType.pingPong));
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
