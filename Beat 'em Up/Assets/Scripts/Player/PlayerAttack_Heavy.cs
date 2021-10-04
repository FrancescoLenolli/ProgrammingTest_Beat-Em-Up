using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack_Heavy : CharacterAttack
{
    [Min(.1f)]
    [SerializeField]
    private float stunTime = 1.0f;

    protected override RaycastHit2D[] Attack()
    {
        RaycastHit2D[] hits = base.Attack();

        foreach (RaycastHit2D hit in hits)
        {
            EnemyControl enemy = hit.collider.GetComponent<EnemyControl>();
            if (enemy)
            {
                enemy.interactingCharacterDirection = transform.right;
                enemy.IsStaggered = true;
                enemy.Health?.Damage(attackValues.damage);
                enemy.Stun(stunTime);
            }
        }

        return hits;
    }
}
