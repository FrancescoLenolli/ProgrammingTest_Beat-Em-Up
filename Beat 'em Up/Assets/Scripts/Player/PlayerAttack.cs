using UnityEngine;

public class PlayerAttack : CharacterAttack
{
    protected override RaycastHit2D[] Attack()
    {
        RaycastHit2D[] hits = base.Attack();

        foreach (RaycastHit2D hit in hits)
        {
            EnemyControl enemy = hit.collider.GetComponent<EnemyControl>();
            if (enemy)
                enemy.Health?.Damage(attackValues.damage);
        }

        return hits;
    }
}
