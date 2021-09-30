using UnityEngine;

[CreateAssetMenu(menuName = "AttackValues", fileName = "NewValues")]
public class AttackValues : ScriptableObject
{
    [Tooltip("How fast can this attack be performed.")]
    [Min(.1f)]
    public float rate;
    [Tooltip("How far can the attack reach.")]
    [Min(.1f)]
    public float range;
    [Tooltip("Vertical attack area. The lower the value, the more the attack act like a ray.")]
    [Min(.1f)]
    public float area;
    [Tooltip("Damage per attack.")]
    [Min(.1f)]
    public float damage = 1.0f;
}
