using UnityEngine;


public class PlayerCrouch : Player
{
    private bool isCrouching;
    PlayerWalk playerWalk;

    private BoxCollider2D crouchCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        playerWalk = GetComponent<PlayerWalk>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        isCrouching = (playerInput.actions["VerticalMovement"].ReadValue<float>() < 0) ? true : false;
        base.Update();
        animator.SetBool("isCrouching", isCrouching);
        playerWalk.speed = isCrouching ? 0f: GameParameters.PlayerSpeed;
    }
}
