using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Attack : State
{
    public override void UpdateState()
    {
        Owner.CharacterAttack.StartAttack();

        StateMachine.SwitchState(typeof(State_CheckRange));
    }
}
