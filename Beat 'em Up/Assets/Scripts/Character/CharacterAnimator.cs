using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    protected Animator animator;
    protected bool canAnimateMovement;
    protected int attackTriggerHash;
    protected int attackHeavyTriggerHash;
    protected int idleTriggerHash;
    protected int walkUpTriggerHash;
    protected int walkDownTriggerHash;
    protected int hitTriggerHash;
    protected int deathTriggerHash;

    /// <summary>
    /// Can the movement animations be player?
    /// </summary>
    public bool CanAnimateMovement { get => canAnimateMovement; set => canAnimateMovement = value; }

    public virtual void SetUp(Animator animator)
    {
        canAnimateMovement = true;
        this.animator = animator;
        // Store the value of the trigger for better performance.
        attackTriggerHash = Animator.StringToHash("Attack");
        attackHeavyTriggerHash = Animator.StringToHash("Attack_Heavy");
        idleTriggerHash = Animator.StringToHash("Idle");
        walkUpTriggerHash = Animator.StringToHash("Walk_Up");
        walkDownTriggerHash = Animator.StringToHash("Walk_Down");
        hitTriggerHash = Animator.StringToHash("Hit");
        deathTriggerHash = Animator.StringToHash("Death");
    }

    public void HandleAnimation(Vector3 input)
    {
        if (!animator || !canAnimateMovement)
            return;

        if (input != Vector3.zero)
        {
            if (input.y > 0)
                animator.SetTrigger(walkUpTriggerHash);
            if (input.y <= 0)
                animator.SetTrigger(walkDownTriggerHash);
        }
        else
        {
            animator.SetTrigger(idleTriggerHash);
        }
    }

    public void AttackAnimation()
    {
        animator.SetTrigger(attackTriggerHash);
    }

    public void AttackHeavyAnimation()
    {
        animator.SetTrigger(attackHeavyTriggerHash);
    }

    public void IdleAnimation()
    {
        animator.SetTrigger(idleTriggerHash);
    }

    public void HitAnimation()
    {
        animator.SetTrigger(hitTriggerHash);
    }

    public void DeathAnimation()
    {
        animator.SetTrigger(deathTriggerHash);
    }
}
