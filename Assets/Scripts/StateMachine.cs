using UnityEngine;
using System;
using Unity.VisualScripting;

public class StateMachine : MonoBehaviour
{

    [SerializeField] private bool enterFirstStateOnStart = true;

    [SerializeField] private bool loggingEnabled = true;
    [SerializeField] private StateNode[] states;

    private StateNode currentState;
    public StateNode[] States => states;

    public StateNode CurrentState => currentState;

    public event Action<StateNode, StateNode> OnStateChange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (states.Length == 0) return;

        foreach (StateNode state in states)
        {
            state.Initialize(this);
        }

        if (enterFirstStateOnStart)
        {
            SetState(states[0]);
        }
    }

    // Update is called once per frame
    public void Next()
    {
        if (states.Length == 0) return;

        int currentIndex = Array.IndexOf(states, currentState);
        int nextIndex = (currentIndex + 1) % states.Length;
        SetState(states[nextIndex]);
    }

    public void SetState(StateNode newState)
    {
        StateNode oldState = currentState;
        currentState?.Exit();
        
        if (loggingEnabled)
        {
            string stateName = oldState == null ? "null state": oldState.GetType().ToString();
            Debug.Log($"Entering{stateName}.");
        }

        currentState = newState;
        currentState?.Enter();
        if (loggingEnabled)
        {
            string stateName = newState == null ? "null state": newState.GetType().ToString();
            Debug.Log($"Entering{stateName}.");
        }
        OnStateChange?.Invoke(currentState, newState);
    }
}
