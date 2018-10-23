using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour {

    private LoadSceneManager loadSceneManager;
    public string sceneName;

    public Vector3 startPos;

    public CheckPointData checkPointData;

    // Use this for initialization
    void Start () {
        loadSceneManager = GameObject.Find("Management").GetComponent<LoadSceneManager>();
    }

    // Update is called once per frame
    public void ClickButton()
    {
        checkPointData.startPosition = startPos;
        loadSceneManager.FadeAndLoadScene(sceneName);
    }
}
