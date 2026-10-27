using System;
using UnityEngine;

public class JumpingNode : StateNode
{
    [SerializeField] private GameObject groundShadow;
    private float jumpSpeed = GameParameters.JumpSpeed;
    private float landTolerance = GameParameters.LandTolerance;
    
    private static readonly int JumpAnimHash = Animator.StringToHash("BoxingJump");
    
    private Rigidbody2D rigidbody2D;
    private float groundYOnJump;
    private float lockedJumpDir;
    
    protected override void Awake()
    {
        base.Awake();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        
        animator.SetBool("isJumping",true);
        groundShadow.SetActive(true);
        
        groundYOnJump = rigidbody2D.position.y;
        
        float direction = playerInput.actions["LateralMovement"].ReadValue<float>();
        lockedJumpDir = direction * stateMachine.GetState<WalkingNode>().speed;
        
        rigidbody2D.linearVelocity = new Vector2(0f, jumpSpeed);

        animator.speed = 0f;
        animator.Play(JumpAnimHash, 0, 0f);
    }

    public override void Exit()
    {
        animator.speed = 1f;
        animator.SetBool("isJumping",false);
        groundShadow.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    private void Update()
    {
        if (!IsCurrentState) return;
        float velY = rigidbody2D.linearVelocity.y;
        float normalizedT;
        if (velY > 0f)
        {
            normalizedT = Mathf.Lerp(0f, 0.5f, 1f - (velY / jumpSpeed));
        }
        else
        {
            float apexHeight = (jumpSpeed * jumpSpeed) / (2f * Math.Abs(Physics2D.gravity.y * rigidbody2D.gravityScale));
            float height = Mathf.Max(0f, rigidbody2D.position.y - groundYOnJump);
            normalizedT = Mathf.Lerp(1f, 0.5f, Mathf.Clamp01(height / apexHeight));
        }
        
        animator.Play(JumpAnimHash, 0, Mathf.Clamp01(normalizedT));
        animator.Update(0f);


    }

    private void FixedUpdate()
    {
        if (!IsCurrentState) return;
        rigidbody2D.linearVelocity = new Vector2(lockedJumpDir, rigidbody2D.linearVelocity.y);
        groundShadow.transform.position = new Vector3(rigidbody2D.position.x, groundYOnJump, groundShadow.transform.position.z);
        bool isFalling = rigidbody2D.linearVelocity.y <= 0f;
        if (isFalling && rigidbody2D.position.y <= groundYOnJump + landTolerance)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            rigidbody2D.position = new Vector2(rigidbody2D.position.x, groundYOnJump);
            animator.speed = 1f;
            stateMachine.SetState(stateMachine.GetState<IdleNode>());
        }
    }
}
