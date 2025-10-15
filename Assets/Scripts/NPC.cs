using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    private NavMeshAgent  agent;
    public Transform destination;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = destination.position;
    }

    void Update()
    {
        
    }
}
