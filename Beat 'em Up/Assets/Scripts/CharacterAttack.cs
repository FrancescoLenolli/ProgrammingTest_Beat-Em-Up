using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField]
    protected float attackRate = 1.0f;
    [SerializeField]
    protected float attackRange = 1.0f;
    [SerializeField]
    protected float attackArea = .4f;

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

    protected virtual void Attack()
    {
        characterAnimator.AttackAnimation();

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackArea, transform.right, attackRange);

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log(hit.collider.name);
        }
    }
}
