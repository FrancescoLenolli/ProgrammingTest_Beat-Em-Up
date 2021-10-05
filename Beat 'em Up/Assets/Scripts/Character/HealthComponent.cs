using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float totalValue;
    private float value;
    private bool healthDepleted = false;
    private bool isInvincible = false;
    private Action onHealthDepleted;
    private Action onDamageReceived;
    private Action<float> onDamageTaken;

    public Action OnHealthDepleted { get => onHealthDepleted; set => onHealthDepleted = value; }
    public Action OnDamageReceived { get => onDamageReceived; set => onDamageReceived = value; }
    public Action<float> OnDamageTaken { get => onDamageTaken; set => onDamageTaken = value; }
    public bool IsInvincible { get => isInvincible; set => isInvincible = value; }
    public bool Depleted { get => healthDepleted; }
    public float TotalValue { get => totalValue; }
    public float Value { get => value; }

    public void Set(float healthValue)
    {
        totalValue = healthValue;
        value = totalValue;
    }

    public void Revive()
    {
        healthDepleted = false;
        value = totalValue;
    }

    public void Damage(float damageValue)
    {
        if (IsInvincible)
        {
            Debug.Log($"{gameObject.name} is invincible and can't be damaged");
            return;
        }

        value = Mathf.Clamp(value -= damageValue, 0, value);

        if (!healthDepleted)
        {
            onDamageTaken?.Invoke(value);

            if (value <= 0.0f)
            {
                onHealthDepleted?.Invoke();
                healthDepleted = true;
            }
            else
            {
                onDamageReceived?.Invoke();
            }
        }
    }
}
