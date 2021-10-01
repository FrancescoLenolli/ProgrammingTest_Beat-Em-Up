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
        float range = Owner.AttackNormal.attackValues.range;
        Vector3 direction = (Owner.Target.transform.position - transform.position);
        Vector3 moveInput = new Vector3(direction.x, direction.y + randomOffset, direction.z).normalized;

        if (CharacterUtilities.IsTargetInRange(transform, Owner.Target.transform, range))
        {
            StateMachine.SwitchState(typeof(State_Attack));
        }
        else
        {
            Owner.characterMovement.HandleMovement(moveInput);
            Owner.CharacterAnimator.HandleAnimation(moveInput);
        }
    }
}
