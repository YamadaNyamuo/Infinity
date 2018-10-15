using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YukaMoveB : MonoBehaviour {

    public enum MOVE_STETA
    {
        NORMAL,
        RIDO
    }

    [TooltipAttribute("Endにつくまでにかかる時間\n pathと同じ数にしてね")]
    public int[] time;
    [TooltipAttribute("再生タイプの設定\n true:ループ再生するよ\n false:ループ再生しない")]
    public bool type;

    private string typeString;

    [TooltipAttribute("動く床の種類\n RIDOォ…にすると乗ったら動き始めるようにできる")]
    public MOVE_STETA moveSteta;
    [TooltipAttribute("LoopTypeの種類\n Noad:ループしない\n Loop:ループ再生する\n PingPong:再生し終わると逆に再生するループ")]
    public iTween.LoopType loopType;
    [TooltipAttribute("EaseTypeの種類\n 動き方を変えられる\n 移動床ならLinearがいいかもしれない\n 多いから自分で調べろ！")]
    public iTween.EaseType easeType;

    public Vector3[] path;
    private int cnt = 1;

    private bool plRidoFlag = false;
    private bool moveFlag = false;
    // 処理が終わったどうかを示すフラグ
    private bool iTweenMoving = false;
    private bool cntFlag = false;
    private int cntPlus = 1;

    // Use this for initialization
    void Start()
    {
        
    }

    
    // 処理が終わったら呼び出され、フラグをクリアする。
    void OnCompleteHandler()
    {
        iTweenMoving = false;

        if (moveSteta == MOVE_STETA.RIDO)
        {
            if (cnt == 0 || cnt == path.Length - 1)
            {
                cntPlus *= (-1);
            }
        }
        cnt += cntPlus;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSteta == MOVE_STETA.NORMAL)
        {
            if (!iTweenMoving && cnt < path.Length)
            {
                if (type)
                {
                    typeString = "looptype";
                    iTween.MoveTo(this.gameObject, iTween.Hash("position", path[cnt], "time", time[cnt], "oncomplete", "OnCompleteHandler", "easeType", easeType, typeString, loopType));
                    iTweenMoving = true;
                    
                }
                else
                {
                    typeString = "easeType";
                    iTween.MoveTo(this.gameObject, iTween.Hash("position", path[cnt], "time", time[cnt], "oncomplete", "OnCompleteHandler", "easeType", easeType));
                    iTweenMoving = true;
                    
                }
            }
        }
        else if (moveSteta == MOVE_STETA.RIDO)
        {
            if (plRidoFlag == true)
            {
                if (!iTweenMoving&& cnt < path.Length)
                {
                    if (type)
                    {
                        typeString = "looptype";
                        iTween.MoveTo(this.gameObject, iTween.Hash("position", path[cnt], "time", time[cnt], "oncomplete", "OnCompleteHandler", "easeType", easeType, typeString, loopType));
                        moveFlag = true;
                        iTweenMoving = true;
                    }
                    else
                    {
                        typeString = "easeType";
                        iTween.MoveTo(this.gameObject, iTween.Hash("position", path[cnt], "time", time[cnt], "oncomplete", "OnCompleteHandler", "easeType", easeType));
                        moveFlag = true;
                        iTweenMoving = true;
                    }
                }
            }
        }
        else
        {

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
