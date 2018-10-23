using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlocking : MonoBehaviour {
    [SerializeField]
    GameObject clearObject;
    // Use this for initialization
    void Start() {
        clearObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Unlocked() {
        clearObject.SetActive(true);
        ChengeColor();
    }

    void ChengeColor() {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
