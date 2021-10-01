using UnityEngine;

[CreateAssetMenu(menuName = "AttackValues", fileName = "NewValues")]
public class AttackValues : ScriptableObject
{
    [Tooltip("How fast can this attack be performed.")]
    [Min(.1f)]
    public float rate = 1.0f;
    [Tooltip("How far can the attack reach.")]
    [Min(.1f)]
    public float range = 1.0f;
    [Tooltip("Damage per attack.")]
    [Min(.1f)]
    public float damage = 1.0f;
}
