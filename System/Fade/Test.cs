using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    public Fade fade;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            fade.FadeIn(0.5f, () => print("フェードイン完了"));
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            fade.FadeOut(0.5f, () => print("フェードアウト完了"));
        }
    }
}
