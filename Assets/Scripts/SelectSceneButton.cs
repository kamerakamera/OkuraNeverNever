using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        DebugLoadScene();
	}

    //デバッグ用の関数
    void DebugLoadScene() {
        if (Input.GetKeyDown("i")) {
            SceneManager.LoadScene("Tutorial");
        }
    }

    public void OnClick(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
