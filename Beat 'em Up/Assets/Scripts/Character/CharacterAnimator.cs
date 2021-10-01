using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;
    private int attackTriggerHash;
    private int attackHeavyTriggerHash;
    private int idleTriggerHash;
    private int walkUpTriggerHash;
    private int walkDownTriggerHash;
    private int hitTriggerHash;
    private int deathTriggerHash;

    public void SetUp(Animator animator)
    {
        this.animator = animator;
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
        if (!animator)
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
