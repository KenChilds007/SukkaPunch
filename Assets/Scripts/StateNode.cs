using UnityEngine;

public abstract class StateNode : MonoBehaviour
{
    protected StateMachine stateMachine;

    public bool IsCurrentState => stateMachine != null && stateMachine.CurrentState == this;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Initialize(StateMachine machine)
    {
        stateMachine = machine;
    }

    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        
    }
}
