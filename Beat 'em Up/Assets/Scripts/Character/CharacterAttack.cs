using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float attackRate;
    public float attackRange;
    public float attackArea;
    public float attackValue;

    protected CharacterAnimator characterAnimator;

    private float attackTimer = .0f;
    private bool canAttack = false;

    private void Update()
    {
        attackTimer = Mathf.Clamp(attackTimer - Time.deltaTime, 0, attackRate);
        if (attackTimer <= 0 && canAttack)
        {
            attackTimer = attackRate;
            canAttack = false;
            Attack();
        }
    }

    public void SetUp(CharacterAnimator characterAnimator)
    {
        this.characterAnimator = characterAnimator;
    }

    public void StartAttack()
    {
        if (attackTimer <= 0)
            canAttack = true;
    }

    protected virtual RaycastHit2D[] Attack()
    {
        characterAnimator.AttackAnimation();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackArea, transform.right, attackRange);

        return hits;
    }
}
