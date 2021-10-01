using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class PlayerControl : CharacterControl
{
    [SerializeField]
    private float healthValue = 2.0f;
    [SerializeField]
    private CharacterAttack attackNormal = null;
    [SerializeField]
    private CharacterAttack attackHeavy = null;

    private Animator animator;
    private CharacterAnimator characterAnimator;
    private HealthComponent health;

    public HealthComponent Health { get => health; set => health = value; }
    public CharacterAttack AttackNormal { get => attackNormal; set => attackNormal = value; }
    public CharacterAttack AttackHeavy { get => attackHeavy; set => attackHeavy = value; }

    protected override void SetUp()
    {
        base.SetUp();
        characterAnimator.SetUp(animator);
        health.Value = healthValue;

        characterInput.ActionMove += characterMovement.HandleMovement;
        characterInput.ActionJump += characterMovement.SetJump;
        characterInput.ActionMove += characterAnimator.HandleAnimation;
        characterInput.ActionAttack1 += attackNormal.StartAttack;
        characterInput.ActionAttack2 += attackHeavy.StartAttack;
        attackNormal.OnAttack += characterAnimator.AttackAnimation;
        attackHeavy.OnAttack += characterAnimator.AttackHeavyAnimation;
        health.OnDamageReceived += characterAnimator.HitAnimation;
        health.OnDamageReceived += BounceBack;
        health.OnHealthDepleted += Die;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        health = CharacterUtilities.TryGetComponent<HealthComponent>(gameObject);
        characterInput = CharacterUtilities.TryGetComponent<CharacterInput>(gameObject);
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
    }

    private void Die()
    {
        //gameObject.SetActive(false);
    }
}
