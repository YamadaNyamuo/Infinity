using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour {

    //アンロードするシーン
    private Scene unLoadScene;

    //シーン用データ
    private SceneData sceneData;

    // Use this for initialization
    IEnumerator Start()
    {
        //最初にTitleシーンを読み込む
        yield return LoadNewScene("TitleScene");
        //SceneDataを保持
        sceneData = FindObjectOfType(typeof(SceneData)) as SceneData;
    }
    public void FadeAndLoadScene(string sceneName)
    {
        sceneData = FindObjectOfType(typeof(SceneData)) as SceneData;
        //個々のシーンのデータを取得
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {

        //現在のシーンデータを取得
        sceneData = FindObjectOfType(typeof(SceneData)) as SceneData;
        //他のシーンへ遷移する時にフェードアウト
        

        Destroy(FindObjectOfType(typeof(AudioListener)));
        unLoadScene = SceneManager.GetActiveScene();

        //フェードアウトが完了したら新しいシーンを読み込む
        yield return StartCoroutine(LoadNewScene(sceneName));

        //フェードアウトが完了したら前のシーンをアンロード
        yield return StartCoroutine(UnLoadScene());

        //現在のシーンデータを取得
        sceneData = FindObjectOfType(typeof(SceneData)) as SceneData;


        ////フェードイン

        
    }

    IEnumerator LoadNewScene(string sceneName)
    {

        //シーン読み込み処理
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!async.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
    }

    //シーンのアンロード
    IEnumerator UnLoadScene()
    {
        yield return SceneManager.UnloadScene(unLoadScene.buildIndex);
        //yield return SceneManager.UnloadSceneAsync(unLoadScene.buildIndex);
    }

}
