using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManage : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform[] generatePoint;
    [SerializeField] private Transform playerTrfm;
    [SerializeField] private bool canGenerate;
    [SerializeField] private float generateInterval;
    [SerializeField] private float minGeneSqrDistance;
    [SerializeField] private float surviveTime;
    private float prevGenerateTime;
    private float destroyTime;

    void Awake()
    {
        SetCanGenerate(true);
        if (playerTrfm == null)
        {
            playerTrfm = GameObject.Find("Player").transform;
        }
    }

    void FixedUpdate()
    {
        if (enemy.activeSelf)
        {
            ActiveCheck();
        }
        else if (canGenerate && Time.fixedTime - destroyTime > generateInterval)
        {
            Generate();
        }
    }

    private void Generate()
    {
        //int index = Random.Range(0, generatePoint.Length);
        enemy.transform.position = generatePoint[searchGeneratePoint()].position;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);
        prevGenerateTime = Time.fixedTime;
    }

    private int searchGeneratePoint()
    {
        int index = 0;
        float nearDistance = 0.0f;

        for (int i = 0; i < generatePoint.Length; i++)
        {
            float testDistance = Vector3.SqrMagnitude(generatePoint[i].position - playerTrfm.position);
            if ((nearDistance == 0.0f || nearDistance > testDistance) && testDistance > minGeneSqrDistance)
            {
                index = i;
                nearDistance = testDistance;
            }
        }

        return index;
    }

    private void ActiveCheck()
    {
        if (Time.fixedTime - prevGenerateTime > surviveTime)
        {
            enemy.SetActive(false);
            destroyTime = Time.fixedTime;
        }
    }

    public void SetCanGenerate(bool value) {
        canGenerate = value;
    }

    public void PlayerDeath() {
        canGenerate = false;
        enemy.SetActive(false);
    }
}
