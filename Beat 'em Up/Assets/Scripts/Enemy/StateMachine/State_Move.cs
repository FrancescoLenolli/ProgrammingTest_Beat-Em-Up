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
        if (!Owner.Target)
            return;

        float range = Owner.AttackNormal.attackValues.range;
        Vector3 direction = (Owner.Target.transform.position - transform.position);
        direction = new Vector3(direction.x, direction.y + randomOffset, direction.z);

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
