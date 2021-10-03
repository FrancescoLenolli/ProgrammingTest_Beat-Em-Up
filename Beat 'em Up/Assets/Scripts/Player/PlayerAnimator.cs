using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    private int deathToIdleTriggerHash;

    public override void SetUp(Animator animator)
    {
        base.SetUp(animator);
        deathToIdleTriggerHash = Animator.StringToHash("Death_To_Idle");
    }

    public void DeathToIdleAnimation()
    {
        animator.SetTrigger(deathToIdleTriggerHash);
    }
}
