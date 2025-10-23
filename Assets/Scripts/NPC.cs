using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, IStateMachine
{
    public IState CurrentState { get; set; }
    
    public NavMeshAgent  agent;
    public Transform destination;
    private Collider[] detectedObjects =  new Collider[10];
    public float detectionRadius = 10f;
    public Transform player;

    public bool isPlayerSighted;
    [Header("Patrolling State Variables")]
    public Transform[] waypoints;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ChangeState(new PatrollingState(this));
    }

    private void FixedUpdate()
    {
        var numberOfColliders = Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, detectedObjects);
        isPlayerSighted = false;
        for (int i = 0; i < numberOfColliders; i++)
        {
            var col = detectedObjects[i];
            if (!col.CompareTag("Player")) continue; // Filtro 1
            player = col.transform;
            var vectorToPlayer = player.position - transform.position;
            var dot = Vector3.Dot(vectorToPlayer, transform.forward);
            if (dot < 0) continue; // Filtro 2
            
            Physics.Raycast(transform.position, vectorToPlayer, out var hit);
            if (hit.collider.transform != player) continue; // Filtro 3
            
            isPlayerSighted = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.04f, 0.92f, 1f, 0.4f);
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }

    void Update()
    {
        CurrentState?.Tick(Time.deltaTime);
    }
    
    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
    
}

public struct PatrollingState : IState
{
    public NPC StateMachine { get; set; }
    
    public PatrollingState(NPC stateMachine)
    {
        StateMachine = stateMachine;
    }
    
    public void Enter()
    {
        if (StateMachine.waypoints.Length == 0) return;
            StateMachine.agent.destination = StateMachine.waypoints[0].position;
    }

    public void Tick(float deltaTime)
    {
    }

    public void Exit()
    {
    }
}
