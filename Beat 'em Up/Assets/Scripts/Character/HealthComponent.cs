using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float value;
    private Action onHealthDepleted;
    private Action onDamageReceived;

    public float Value { get => value; set => this.value = value; }
    public Action OnHealthDepleted { get => onHealthDepleted; set => onHealthDepleted = value; }
    public Action OnDamageReceived { get => onDamageReceived; set => onDamageReceived = value; }

    public void Damage(float damageValue)
    {
        value = Mathf.Clamp(value -= damageValue, 0, value);
        onDamageReceived?.Invoke();
        if (value <= 0.0f)
            onHealthDepleted?.Invoke();
    }
}
