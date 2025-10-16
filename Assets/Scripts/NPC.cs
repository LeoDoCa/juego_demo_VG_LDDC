using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private NavMeshAgent  agent;
    public Transform destination;
    private Collider[] detectedObjects =  new Collider[10];
    public float detectionRadius = 10f;
    private Transform player;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
    }

    private void FixedUpdate()
    {
        var numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, detectedObjects);
        player = null;
        for (int i = 0; i < numberOfColliders; i++)
        {
            var col = detectedObjects[i];
            if (!col.CompareTag("Player")) continue;
            player = col.transform;
            var vectorToPlayer = player.position - transform.position;
            var dot = Vector3.Dot(vectorToPlayer, transform.forward);
            if (dot < 0) continue;
            
            Physics.Raycast(transform.position, vectorToPlayer, out var hit);
            if (hit.collider.transform != player) continue;
            
            agent.destination = player.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.04f, 0.92f, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }

    void Update()
    {
        
    }
}
