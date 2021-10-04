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
        Debug.Log("Can Move");
        playerAnimator.CanAnimateMovement = true;
    }

    protected override void SetUp()
    {
        base.SetUp();
        playerAnimator.SetUp(animator);
        health.Set(healthValue);

        characterInput.ActionMove += characterMovement.HandleMovement;
        characterInput.ActionJump += characterMovement.SetJump;
        characterInput.ActionMove += playerAnimator.HandleAnimation;
        characterInput.Action1 += attackNormal.StartAttack;
        characterInput.Action2 += attackHeavy.StartAttack;
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
        health = CharacterUtilities.TryGetComponent<HealthComponent>(gameObject);
        characterInput = CharacterUtilities.TryGetComponent<CharacterInput>(gameObject);
        playerAnimator = CharacterUtilities.TryGetComponent<PlayerAnimator>(gameObject);
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
    }
}
