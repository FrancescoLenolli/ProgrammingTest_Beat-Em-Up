using UnityEngine;

public class EnemyAttack : CharacterAttack
{
    protected override RaycastHit2D[] Attack()
    {
        RaycastHit2D[] hits = base.Attack();

        foreach (RaycastHit2D hit in hits)
        {
            PlayerControl player = hit.collider.GetComponent<PlayerControl>();
            if (player)
                player.Health?.Damage(attackValues.damage);
        }

        return hits;
    }
}
