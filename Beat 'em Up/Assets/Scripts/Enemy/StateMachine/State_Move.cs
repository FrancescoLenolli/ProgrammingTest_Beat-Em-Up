using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Move : State
{
    public override void UpdateState()
    {
        if (!Owner.Target)
            return;

        bool isTargetFacingEnemy = Owner.Target.transform.right.x > 0;
        float range = Owner.AttackComponent.attackRange;
        Vector3 direction = (Owner.Target.transform.position - transform.position);
        Vector3 moveInput = direction.normalized;
        float distance = direction.magnitude;

        if (distance <= range)
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
