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
    private CharacterAnimator characterAnimator;
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
        characterAnimator.DeathToIdleAnimation();
        characterMovement.canMove = true;
        Debug.Log("Can Move");
        characterAnimator.CanAnimate = true;
    }

    protected override void SetUp()
    {
        base.SetUp();
        characterAnimator.SetUp(animator);
        health.Set(healthValue);

        characterInput.ActionMove += characterMovement.HandleMovement;
        characterInput.ActionJump += characterMovement.SetJump;
        characterInput.ActionMove += characterAnimator.HandleAnimation;
        characterInput.Action1 += attackNormal.StartAttack;
        characterInput.Action2 += attackHeavy.StartAttack;
        attackNormal.OnAttack += characterAnimator.AttackAnimation;
        attackHeavy.OnAttack += characterAnimator.AttackHeavyAnimation;
        health.OnDamageReceived += characterAnimator.HitAnimation;
        health.OnDamageReceived += Stagger;
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

    private void Stagger()
    {
        BounceBack(staggerTime);
    }

    private void Die()
    {
        isAlive = false;
        characterMovement.canMove = false;
        characterAnimator.CanAnimate = false;
        characterAnimator.DeathAnimation();
    }
}
