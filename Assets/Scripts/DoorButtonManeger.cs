using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonManeger : MonoBehaviour {
    private int unlockButtonCount;
    [SerializeField]
    private int lockButtonLimit;
    [SerializeField]
    GameObject ClearObj;
	// Use this for initialization
	void Start () {
        ClearObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckUnlockButtonCount() {
        if(unlockButtonCount >= lockButtonLimit) {
            Unlocking();
        }
    }

    void Unlocking() {
        ClearObj.SetActive(true);
    }

    public void AddLockButtonCount() {
        unlockButtonCount++;
        CheckUnlockButtonCount();
    } 
}
