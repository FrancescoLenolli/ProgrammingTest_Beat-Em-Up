﻿using UnityEngine;

public class State_Attack : State
{
    public override void UpdateState()
    {
        if (!Owner.Target)
            return;

        bool targetOnLeftSide = Owner.Target.transform.position.x < transform.position.x;

        transform.rotation = targetOnLeftSide ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

        Owner.AttackComponent.StartAttack();
        Owner.CharacterAnimator.IdleAnimation();

        StateMachine.SwitchState(typeof(State_Move));
    }
}
