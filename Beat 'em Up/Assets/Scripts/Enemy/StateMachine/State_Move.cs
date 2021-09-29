using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Move : State
{
    public override void UpdateState()
    {
        Vector3 direction = (Owner.Target.transform.position - transform.position).normalized;
        Owner.characterMovement.HandleMovement(direction);
        Owner.CharacterAnimator.HandleAnimation(direction);

        StateMachine.SwitchState(typeof(State_CheckRange));
    }
}
