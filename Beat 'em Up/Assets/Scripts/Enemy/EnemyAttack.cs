using UnityEngine;

public class EnemyAttack : CharacterAttack
{
    public override RaycastHit2D[] Attack()
    {
        RaycastHit2D[] hits = base.Attack();

        foreach (RaycastHit2D hit in hits)
        {
            PlayerControl player = hit.collider.GetComponent<PlayerControl>();
            if (player)
            {
                // Tell the player the direction of the attack to apply the BounceBack status correctly.
                player.interactingCharacterDirection = transform.right;
                player.Health?.Damage(attackValues.damage);
            }
        }

        return hits;
    }
}
