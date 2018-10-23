using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManegement : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

	}
	
	// Update is called once per frame
	void Update () {
        GameExit();
	}

    void GameExit() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void GameEnd() {
        SceneManager.LoadScene("GameOver");
    }

    public void GameClear() {
        SceneManager.LoadScene("GameClear");
    }
}
