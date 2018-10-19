using UnityEngine;
using System.Collections;

public class WarpAlienFadeInOutScript : MonoBehaviour
{

    Color alpha = new Color(0, 0, 0, 0.02f);
    new Renderer renderer;

    WarpAlienMove warpAlienMove;

    // Use this for initialization
    void Start()
    {
        renderer = GetComponent<Renderer>();
        warpAlienMove = GetComponentInParent<WarpAlienMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if(warpAlienMove.state == WarpAlienMove.WARP_ALIEN_STATE.WARP && warpAlienMove.warpFlag == false) // プレイヤーを見失ったらフェードアウト
        {
            FadeOut();
        }

        if ((warpAlienMove.state != WarpAlienMove.WARP_ALIEN_STATE.WARP && renderer.material.color.a <= 1) || warpAlienMove.warpFlag == true) // プレイヤーの上に出てきてフェードイン
        {
            FadeIn();
        }
    }

    // フェードイン
    public void FadeIn()
    {
        if (renderer.material.color.a <= 1)
        {
            renderer.material.color += alpha;
        }
    }

    // フェードアウト
    public void FadeOut()
    {
        if (renderer.material.color.a >= 0)
        {
            renderer.material.color -= alpha;
        }
    }
}
