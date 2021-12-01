using UnityEngine;

public class EnemyAnimator : CharacterAnimator
{
    private int stunTriggerHash;
    private int stunToIdleTriggerHash;

    public override void SetUp(Animator animator)
    {
        base.SetUp(animator);
        stunToIdleTriggerHash = Animator.StringToHash("Stun_To_Idle");
        stunTriggerHash = Animator.StringToHash("Stun");
    }

    public void StunAnimation()
    {
        animator.SetTrigger(stunTriggerHash);
    }

    public void StunToIdleAnimation()
    {
        animator.SetTrigger(stunToIdleTriggerHash);
    }
}
