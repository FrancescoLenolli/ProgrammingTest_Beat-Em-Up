using UnityEngine;

public class PlayerAttack : CharacterAttack
{
    public override RaycastHit2D[] Attack()
    {
        RaycastHit2D[] hits = base.Attack();

        foreach (RaycastHit2D hit in hits)
        {
            EnemyControl enemy = hit.collider.GetComponent<EnemyControl>();
            if (enemy)
            {
                enemy.interactingCharacterDirection = transform.right;
                enemy.Health?.Damage(attackValues.damage);
            }
        }

        return hits;
    }
}
