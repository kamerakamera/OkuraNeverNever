using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneButton : MonoBehaviour {
    [SerializeField]
    GameObject[] descriptionPage;
    bool isDescription;
    int pageNum;
	// Use this for initialization
	void Start () {
		foreach(GameObject obj in descriptionPage) {
            obj.SetActive(false);
        }
        isDescription = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (!isDescription && Input.GetKeyDown(KeyCode.Space)) {
            LoadScene();
        }
        if (Input.GetKeyDown("q") && !isDescription) {
            Description();
        }
        if(isDescription && Input.GetKeyDown(KeyCode.Space)) {
            NextPage();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        
	}

    void Description() {
        descriptionPage[0].SetActive(true);
        isDescription = true;
        pageNum = 0;
    }

    void NextPage() {
        descriptionPage[pageNum].SetActive(false);
        pageNum++;
        if(pageNum >= descriptionPage.Length) {
            isDescription = false;
        } else {
            descriptionPage[pageNum].SetActive(true);
        }
    }
    
    void LoadScene() {
        SceneManager.LoadScene("SampleScene");
        
    }

    public void OnClick(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
