using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform[] generatePoint;
    [SerializeField] private Transform playerTrfm;
    [SerializeField] private bool canGenerate;
    [SerializeField] private float generateInterval;
    private float prevGenerateTime;

    void Awake()
    {
        if (playerTrfm == null)
        {
            playerTrfm = GameObject.Find("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (canGenerate && Time.fixedTime - prevGenerateTime > generateInterval)
        {
            Generate();
        }
    }

    private void Generate()
    {
        int index = Random.Range(0, generatePoint.Length);
        Instantiate(enemy, generatePoint[index].position, Quaternion.identity);
        prevGenerateTime = Time.fixedTime;
        canGenerate = false;
    }
}
