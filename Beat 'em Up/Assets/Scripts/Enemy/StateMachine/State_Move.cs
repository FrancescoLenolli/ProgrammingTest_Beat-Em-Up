using CoreCharacter.Utilities;
using UnityEngine;

public class State_Move : State
{
    private float randomOffset;

    public override void SetUp(EnemyControl owner, StateMachine stateMachine)
    {
        base.SetUp(owner, stateMachine);
        randomOffset = Random.Range(-.2f, .2f);
    }

    public override void UpdateState()
    {
        if (Owner && Owner.Target && Owner.Target.IsAlive && !Owner.IsStunned && !Owner.IsStaggered)
        {

            float range = Owner.AttackNormal.attackValues.range;
            Vector3 direction = (Owner.Target.transform.position - transform.position);
            Vector3 moveInput = new Vector3(direction.x, direction.y + randomOffset, direction.z).normalized;

            if (CharacterUtilities.IsTargetInRange(transform, Owner.Target.transform, range))
            {
                StateMachine.SwitchState(typeof(State_Attack));
            }
            else
            {
                Move(moveInput);
            }
        }
        else
        {
            // If there's no owner, target or the target is dead, don't move.
            Move(Vector3.zero);
        }
    }

    private void Move(Vector3 moveInput)
    {
        Owner.characterMovement.HandleMovement(moveInput);
        Owner.EnemyAnimator.HandleAnimation(moveInput);
    }
}
