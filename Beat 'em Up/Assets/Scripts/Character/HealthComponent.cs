using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float value;
    private Action onHealthDepleted;

    public Action OnHealthDepleted { get => onHealthDepleted; set => onHealthDepleted = value; }
    public float Value { get => value; set => this.value = value; }

    public void Damage(float damageValue)
    {
        Debug.Log($"{gameObject.name} health is {value}");

        value = Mathf.Clamp(value -= damageValue, 0, value);

        if (value <= 0.0f)
            onHealthDepleted?.Invoke();
    }
}
