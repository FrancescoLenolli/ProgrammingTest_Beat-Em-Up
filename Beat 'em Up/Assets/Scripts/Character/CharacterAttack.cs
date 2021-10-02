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
            ResetTimer();
            canAttack = false;
            Attack();
        }
    }

    public void ResetTimer()
    {
        attackTimer = attackValues.rate;
    }

    public void StartAttack()
    {
        if (attackTimer <= 0)
            canAttack = true;
    }

    protected virtual RaycastHit2D[] Attack()
    {
        onAttack?.Invoke();
        Vector3 origin = transform.position;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, attackValues.range / 2, transform.right, attackValues.range / 2);

        return hits;
    }
}
