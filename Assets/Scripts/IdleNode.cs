using UnityEngine;
public class IdleNode : StateNode
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        base.Awake();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!IsCurrentState) return;
        
        if (playerInput.actions["VerticalMovement"].ReadValue<float>() < 0f)
        {
            stateMachine.SetState(stateMachine.GetState<CrouchingNode>());
            return;
        }
        
        float moveDir = playerInput.actions["LateralMovement"].ReadValue<float>();
        if (moveDir != 0f)
        {
            stateMachine.SetState(stateMachine.GetState<WalkingNode>());
        }
    }
}
