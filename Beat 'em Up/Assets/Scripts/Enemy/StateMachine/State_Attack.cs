using UnityEngine;

public class State_Attack : State
{
    private bool normalAttack = true;

    public override void UpdateState()
    {
        if (!Owner.Target)
            return;

        bool targetOnLeftSide = Owner.Target.transform.position.x < transform.position.x;
        transform.rotation = targetOnLeftSide ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);

        // Repeat a sequence of normalAttack/heavyAttack;
        if (normalAttack)
        {
            Owner.AttackNormal.StartAttack();
            normalAttack = false;
        }
        else
        {
            Owner.AttackHeavy.StartAttack();
            normalAttack = true;
        }
        Owner.CharacterAnimator.IdleAnimation();

        StateMachine.SwitchState(typeof(State_Move));
    }
}
