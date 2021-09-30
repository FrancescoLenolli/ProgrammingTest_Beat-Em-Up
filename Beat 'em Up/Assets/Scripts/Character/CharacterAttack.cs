using System;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public AttackValues attackValues;
    private float attackTimer = .0f;
    private bool canAttack = false;
    private Action onAttack;

    public Action OnAttack { get => onAttack; set => onAttack = value; }

    private void Update()
    {
        attackTimer = Mathf.Clamp(attackTimer - Time.deltaTime, 0, attackValues.rate);
        if (attackTimer <= 0 && canAttack)
        {
            attackTimer = attackValues.rate;
            canAttack = false;
            Attack();
        }
    }

    public void StartAttack()
    {
        if (attackTimer <= 0)
            canAttack = true;
    }

    protected virtual RaycastHit2D[] Attack()
    {
        onAttack?.Invoke();
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, attackValues.area, transform.right, attackValues.range);

        return hits;
    }
}
