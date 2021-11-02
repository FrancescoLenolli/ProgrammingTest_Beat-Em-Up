using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class PlayerControl : CharacterControl
{
    [SerializeField]
    private float healthValue = 2.0f;
    [Tooltip("Amount of time the Player staggers back when hit.")]
    [SerializeField]
    private float staggerTime = .5f;
    [SerializeField]
    private CharacterAttack attackNormal = null;
    [SerializeField]
    private CharacterAttack attackHeavy = null;

    private bool isAlive = true;
    private Animator animator;
    private PlayerAnimator playerAnimator;
    private HealthComponent health;
    private RenderOrder renderOrder;

    public HealthComponent Health { get => health; set => health = value; }
    public CharacterAttack AttackNormal { get => attackNormal; set => attackNormal = value; }
    public CharacterAttack AttackHeavy { get => attackHeavy; set => attackHeavy = value; }
    public bool IsAlive { get => isAlive; set => isAlive = value; }

    public void Restart(Vector3 position)
    {
        transform.position = position;
        isAlive = true;
        health.Revive();
        playerAnimator.DeathToIdleAnimation();
        characterMovement.CanMove = true;
        playerAnimator.CanAnimateMovement = true;
        renderOrder.ResetHalfHeight();
    }

    protected override void SetUp()
    {
        base.SetUp();
        playerAnimator.SetUp(animator);
        health.Set(healthValue);

        characterInput.ActionMove += characterMovement.HandleMovement;
        characterInput.ActionJump += characterMovement.SetJump;
        characterInput.ActionMove += playerAnimator.HandleAnimation;
        characterInput.Action1 += NormalAttack;
        characterInput.Action2 += HeavyAttack;
        attackNormal.OnAttack += playerAnimator.AttackAnimation;
        attackHeavy.OnAttack += playerAnimator.AttackHeavyAnimation;
        health.OnDamageReceived += playerAnimator.HitAnimation;
        health.OnDamageReceived += Stagger;
        health.OnHealthDepleted += Die;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        renderOrder = GetComponent<RenderOrder>();
        health = CharacterUtilities.TryGetComponent<HealthComponent>(gameObject);
        characterInput = CharacterUtilities.TryGetComponent<CharacterInput>(gameObject);
        playerAnimator = CharacterUtilities.TryGetComponent<PlayerAnimator>(gameObject);
    }

    private void NormalAttack()
    {
        if (isAlive)
            attackNormal.StartAttack();
    }

    private void HeavyAttack()
    {
        if (isAlive)
            attackHeavy.StartAttack();
    }

    private void Stagger()
    {
        BounceBack(staggerTime);
    }

    private void Die()
    {
        isAlive = false;
        characterMovement.CanMove = false;
        playerAnimator.CanAnimateMovement = false;
        playerAnimator.DeathAnimation();
        renderOrder.ResetHalfHeight();
    }
}
