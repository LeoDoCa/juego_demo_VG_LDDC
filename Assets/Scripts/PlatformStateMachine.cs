using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformStateMachine : MonoBehaviour, IStateMachine
{
    public IState CurrentState { get; set; }

    private void Start() => ChangeState(new WaitingState(this, 3f, true)); 

    public void ChangeState(IState newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }

    void Update() => CurrentState?.Tick(Time.deltaTime);
}

public struct WaitingState : IState
{
    public PlatformStateMachine StateMachine { get; set; }
    private float waitTime;
    private float timer;
    private bool moveRightNext; 

    public WaitingState(PlatformStateMachine stateMachine, float waitTime, bool moveRightNext)
    {
        StateMachine = stateMachine;
        this.waitTime = waitTime;
        this.moveRightNext = moveRightNext;
        timer = 0f;
    }

    public void Enter()
    {
        timer = 0f;
        Debug.Log("Enter Waiting State");
    }

    public void Tick(float deltaTime)
    {
        timer += deltaTime;
        if (timer >= waitTime)
        {
            if (moveRightNext)
                StateMachine.ChangeState(new MovingPlatformState(StateMachine, 3f, Vector3.right, false)); 
            else
                StateMachine.ChangeState(new MovingPlatformState(StateMachine, 3f, Vector3.left, true));  
        }
    }

    public void Exit() => Debug.Log("Exit Waiting State");
}

public struct MovingPlatformState : IState
{
    public PlatformStateMachine StateMachine { get; set; }
    private float moveDuration;
    private float timer;
    private Vector3 direction;
    private bool moveRightNext; 

    public MovingPlatformState(PlatformStateMachine stateMachine, float moveDuration, Vector3 direction, bool moveRightNext)
    {
        StateMachine = stateMachine;
        this.moveDuration = moveDuration;
        this.direction = direction;
        this.moveRightNext = moveRightNext;
        timer = 0f;
    }

    public void Enter()
    {
        timer = 0f;
        Debug.Log($"Enter Moving State ({direction})");
    }

    public void Tick(float deltaTime)
    {
        timer += deltaTime;
        StateMachine.transform.Translate(direction * (deltaTime * 2f)); 

        if (timer >= moveDuration)
        {
            StateMachine.ChangeState(new WaitingState(StateMachine, 3f, moveRightNext));
        }
    }

    public void Exit() => Debug.Log("Exit Moving State");
}