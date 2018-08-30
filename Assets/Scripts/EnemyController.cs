using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform targetTrfm;
    [SerializeField] private string targetName = "";

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        if (targetTrfm == null)
        {
            targetTrfm = GameObject.Find(targetName).transform;
        }
    }

    void FixedUpdate()
    {
        if (targetTrfm != null)
        {
            navMeshAgent.SetDestination(targetTrfm.position);
        }
    }

    public void Stop()
    {
        navMeshAgent.isStopped = true;
    }

    public void Restart()
    {
        navMeshAgent.isStopped = false;
    }
}
