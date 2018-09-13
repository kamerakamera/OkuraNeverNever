using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlooking : MonoBehaviour {
    GameObject clearObject;

	// Use this for initialization
	void Start () {
        clearObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Unlocked() {
        clearObject.SetActive(true);
    }
}
