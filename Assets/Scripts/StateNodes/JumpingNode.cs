using UnityEngine;

public class JumpingNode : StateNode
{
    [Range(0f, 1f)]
    [SerializeField] private float freezeNormalizedTime = 0.5f;
    [SerializeField] private GameObject groundShadow;
    [SerializeField] private float jumpSpeed = GameParameters.JumpSpeed;
    [SerializeField] private float landTolerance = 0.02f;
    
    private Rigidbody2D rigidbody2D;
    private float groundYOnJump;
    private float lockedJumpDir;
    private bool frozen;
    
    protected override void Awake()
    {
        base.Awake();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void Enter()
    {
        animator.speed = 1f;
        frozen = false;
        animator.SetBool("isJumping",true);
        groundShadow.SetActive(true);
        
        groundYOnJump = rigidbody2D.position.y;
        
        float direction = playerInput.actions["LateralMovement"].ReadValue<float>();
        lockedJumpDir = direction * stateMachine.GetState<WalkingNode>().speed;
        
        rigidbody2D.linearVelocity = new Vector2(0f, jumpSpeed);
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
        if (!IsCurrentState || frozen) return;
        
        AnimatorStateInfo animInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (animInfo.IsName("BoxingJump") && animInfo.normalizedTime >= freezeNormalizedTime)
        {
            animator.Play("BoxingJump", 0, freezeNormalizedTime);
            animator.speed = 0f;
            frozen = true;
        }
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
