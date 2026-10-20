using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private bool isFacingRight = true;
    public Transform target;
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
