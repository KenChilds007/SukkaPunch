using UnityEngine;

public class CrouchingNode : StateNode
{

    public override void Enter()
    {
        animator.SetBool("isCrouching", true);
    }

    public override void Exit()
    {
        animator.SetBool("isCrouching", false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    void Update()
    {
        if (!IsCurrentState) return;
        
        if (playerInput.actions["VerticalMovement"].ReadValue<float>() == 0)
        {
            stateMachine.SetState(stateMachine.GetState<IdleNode>());
        }
    }
// Update is called once per frame
    
}
