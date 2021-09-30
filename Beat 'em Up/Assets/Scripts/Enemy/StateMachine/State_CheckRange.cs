using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CheckRange : State
{
    public override void UpdateState()
    {
        float range = Owner.AttackNormal.attackRange;
        float distance = (Owner.Target.transform.position - transform.position).magnitude;

        if (distance <= range)
            StateMachine.SwitchState(typeof(State_Attack));
        else
            StateMachine.SwitchState(typeof(State_Move));
    }
}
