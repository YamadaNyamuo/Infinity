using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukaMove : MonoBehaviour {

    public enum MOVE_STETA
    {
        NORMAL,
        RIDO
    }

    [TooltipAttribute("Endにつくまでにかかる時間")]
    public int time = 100;
    [TooltipAttribute("使いたいPathの名前")]
    public string PathName = "Kari";
    [TooltipAttribute("再生タイプの設定\n true:ループ再生するよ\n false:ループ再生しない")]
    public bool type;

    private string typeString;

    [TooltipAttribute("動く床の種類\n RIDOォ…にすると乗ったら動き始めるようにできる")]
    public MOVE_STETA moveSteta;
    [TooltipAttribute("LoopTypeの種類\n Noad:ループしない\n Loop:ループ再生する\n PingPong:再生し終わると逆に再生するループ")]
    public iTween.LoopType loopType;
    [TooltipAttribute("EaseTypeの種類\n 動き方を変えられる\n 移動床ならLinearがいいかもしれない\n 多いから自分で調べろ！")]
    public iTween.EaseType easeType;

    private bool plRidoFlag = false;
    private bool moveFlag = false;

    // Use this for initialization
    void Start()
    {
        if(moveSteta==MOVE_STETA.NORMAL)
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSteta == MOVE_STETA.RIDO)
        {
            if (plRidoFlag == true)
            {
                if(moveFlag == false)
                {
                    if (type)
                    {
                        typeString = "looptype";
                        iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", easeType, typeString, loopType));
                        moveFlag = true;
                    }
                    else
                    {
                        typeString = "easeType";
                        iTween.MoveTo(this.gameObject, iTween.Hash("path", iTweenPath.GetPath(PathName), "time", time, "easeType", easeType));
                        moveFlag = true;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent.SetParent(transform);
            plRidoFlag = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    { 
        if (other.tag == "Player")
        {
            other.transform.parent.SetParent(null);
            plRidoFlag = false;
        }
    }
}
