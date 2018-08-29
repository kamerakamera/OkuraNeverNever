using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform targetTrfm;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        navMeshAgent.SetDestination(targetTrfm.position);
    }
}
