using UnityEngine;
public class PlayerWalk : Player
{
    /*
    private bool isWalking = false;
    public float speed = GameParameters.PlayerSpeed;

    private float moveDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        isWalking = (moveDir == 0) ? false : true;
        moveDir = playerInput.actions["LateralMovement"].ReadValue<float>();
        base.Update();
        if (isWalking)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    void FixedUpdate()
    {
        Vector2 velocity = new Vector2(moveDir * speed, 0f);
        rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.fixedDeltaTime);
    }
    */
    
}
