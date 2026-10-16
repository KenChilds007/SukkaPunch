using UnityEngine;
using System.Collections;

public class PlayerAttack : Player
{
    private int currentAttack;
    public AnimationClip jab;
    private float jabLength;
    private PlayerWalk playerWalk;
    
    public BoxCollider2D hitBox;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        hitBox.enabled = false;
        jabLength = jab.length;
        playerWalk = GetComponent<PlayerWalk>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (playerInput.actions["Jab"].WasPressedThisFrame())
        {
            Jab();
        }
    }

    private IEnumerator EndAttackTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        animator.SetInteger("StandingAttack", 0);
        hitBox.enabled = false;
        playerWalk.speed = GameParameters.PlayerSpeed;
    }
    private void Jab()
    {
        playerWalk.speed = 0;
        animator.SetInteger("StandingAttack",1);
        StartCoroutine(EndAttackTimer(jab.length));
    }
}
