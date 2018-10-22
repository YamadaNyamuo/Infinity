using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour {

    private LoadSceneManager loadSceneManager;
    public string sceneName;

    // Use this for initialization
    void Start () {
        loadSceneManager = GameObject.Find("Management").GetComponent<LoadSceneManager>();
    }

    // Update is called once per frame
    public void ClickButton()
    {
        loadSceneManager.FadeAndLoadScene(sceneName);
    }
}
