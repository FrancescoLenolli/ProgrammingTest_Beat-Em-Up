using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float totalValue;
    private float value;
    private bool healthDepleted = false;
    private Action onHealthDepleted;
    private Action onDamageReceived;

    public Action OnHealthDepleted { get => onHealthDepleted; set => onHealthDepleted = value; }
    public Action OnDamageReceived { get => onDamageReceived; set => onDamageReceived = value; }


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
        value = Mathf.Clamp(value -= damageValue, 0, value);

        if (!healthDepleted)
        {
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
