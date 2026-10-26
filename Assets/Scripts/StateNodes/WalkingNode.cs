using UnityEngine;
public class WalkingNode : StateNode
{
    public float speed = GameParameters.PlayerSpeed;
    
    private Rigidbody2D rigidbody2D;

    private float moveDir;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        base.Awake();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public override void Enter()
    {
        animator.SetBool("isWalking", true);
    }

    public override void Exit()
    {
        animator.SetBool("isWalking", false);
        moveDir = 0;
    }

    private void Update()
    {
        if (!IsCurrentState) return;
        
        if (playerInput.actions["VerticalMovement"].ReadValue<float>() < 0f)
        {
            stateMachine.SetState(stateMachine.GetState<CrouchingNode>());
            return;
        }
        if (playerInput.actions["VerticalMovement"].ReadValue<float>() > 0f)
        {
            stateMachine.SetState(stateMachine.GetState<JumpingNode>());
            return;
        }
        
        moveDir = playerInput.actions["LateralMovement"].ReadValue<float>();
        if (moveDir == 0f)
        {
            stateMachine.SetState(stateMachine.GetState<IdleNode>());
        }
    }

    private void FixedUpdate()
    {
        if (!IsCurrentState) return;
        Vector2 velocity = new Vector2(moveDir * speed, 0f);
        rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.fixedDeltaTime);
    }
}
