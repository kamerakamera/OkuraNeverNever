using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButton : MonoBehaviour {
    // Use this for initialization
    [SerializeField]
    DoorButtonManeger doorButtonManeger;
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Accept() {
        ChengeColor();
        doorButtonManeger.AddLockButtonCount();
    }

    void ChengeColor() {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
