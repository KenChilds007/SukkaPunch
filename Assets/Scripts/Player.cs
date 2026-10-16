using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    protected Rigidbody2D rigidbody2D;
    protected Animator animator;
    private bool isFacingRight = true;
    public Transform target;
    protected Vector2 movement;
    public PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FaceTarget();
    }
    public void FaceTarget()
    {
        if (target == null) return;

        bool targetIsRight = target.position.x > transform.position.x;

        if (targetIsRight != isFacingRight)
        {
            isFacingRight = targetIsRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
}
