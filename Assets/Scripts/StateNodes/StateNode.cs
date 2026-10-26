using UnityEngine;
using UnityEngine.InputSystem;
public abstract class StateNode : MonoBehaviour
{
    protected StateMachine stateMachine;
    protected Animator animator;
    protected PlayerInput playerInput;
    public bool IsCurrentState => stateMachine != null && stateMachine.CurrentState == this;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }
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
