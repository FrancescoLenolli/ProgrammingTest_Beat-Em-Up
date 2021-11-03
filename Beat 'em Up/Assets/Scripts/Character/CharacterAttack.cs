﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        }
    }

    public void ResetTimer()
    {
        attackTimer = attackValues.rate;
    }

    public void StartAttack()
    {
        if (attackTimer <= 0)
        {
            canAttack = true;
            onAttack?.Invoke();
        }
    }

    public virtual RaycastHit2D[] Attack()
    {
        Vector3 origin = transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, transform.right, attackValues.range);

        // Remove this character's transform from the list of colliders detected.
        List<RaycastHit2D> listHits = hits.ToList();
        listHits.RemoveAll(hit => hit.collider.transform == transform);
        hits = listHits.ToArray();

        return hits;
    }
}
