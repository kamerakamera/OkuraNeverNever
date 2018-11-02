using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneManegement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        BackTitleScene();
	}

    void BackTitleScene() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("Start");
        }
    }
}
