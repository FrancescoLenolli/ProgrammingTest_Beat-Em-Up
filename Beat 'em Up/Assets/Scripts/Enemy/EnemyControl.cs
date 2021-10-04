using CoreCharacter;
using CoreCharacter.Utilities;
using System.Collections;
using UnityEngine;

public class EnemyControl : CharacterControl
{
    [Tooltip("Set to TRUE if you're testing the project.\nThe enemy doesn't need a LevelManager to work.")]
    [SerializeField]
    private bool debug = false;
    [SerializeField]
    private float healthValue = 1.0f;
    [Tooltip("Amount of time the Player staggers back when hit.")]
    [SerializeField]
    private float staggerTime = .5f;
    [SerializeField]
    private CharacterAttack attackNormal = null;
    [SerializeField]
    private CharacterAttack attackHeavy = null;

    private Animator animator;
    private EnemyAnimator enemyAnimator;
    private PlayerControl target;
    private HealthComponent health;
    private StateMachine stateMachine;
    private bool isStunned = false;

    public PlayerControl Target { get => target; }
    public EnemyAnimator EnemyAnimator { get => enemyAnimator; }
    public HealthComponent Health { get => health; }
    public CharacterAttack AttackNormal { get => attackNormal; }
    public CharacterAttack AttackHeavy { get => attackHeavy; }
    public bool IsStunned { get => isStunned; }

    private void Awake()
    {
        if (debug)
        {
            target = FindObjectOfType<PlayerControl>();
            SetUp();
        }
    }

    public void Init(LevelManager levelManager, PlayerControl target, Vector3 startingPosition)
    {
        this.target = target;
        transform.position = startingPosition;
        SetUp();
        health.OnHealthDepleted += levelManager.EnemyDied;
    }

    public void Stun(float stunTime)
    {
        if (isStunned || health.Depleted)
            return;

        StartCoroutine(StunRoutine(stunTime));
    }

    protected override void SetUp()
    {
        base.SetUp();
        attackNormal.ResetTimer();
        attackHeavy.ResetTimer();
        enemyAnimator.SetUp(animator);
        attackNormal.OnAttack += enemyAnimator.AttackAnimation;
        attackHeavy.OnAttack += enemyAnimator.AttackHeavyAnimation;
        health.Set(healthValue);
        health.OnDamageReceived += enemyAnimator.HitAnimation;
        health.OnDamageReceived += Stagger;
        health.OnHealthDepleted += Die;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        health = CharacterUtilities.TryGetComponent<HealthComponent>(gameObject);
        enemyAnimator = CharacterUtilities.TryGetComponent<EnemyAnimator>(gameObject);
        stateMachine = GetComponent<StateMachine>();
    }

    private void Stagger()
    {
        BounceBack(staggerTime);
    }

    private void Die()
    {
        stateMachine.Stop();
        enemyAnimator.DeathAnimation();
    }

    private IEnumerator StunRoutine(float stunTime)
    {
        isStunned = true;
        isStaggered = true;
        float timer = stunTime;
        float bounceBackTimer = timer / 2;
        float speed = 1f;
        characterMovement.CanMove = false;
        health.IsInvincible = true;

        enemyAnimator.StunAnimation();

        // Bounce back for a while
        while (timer > bounceBackTimer)
        {
            timer -= Time.deltaTime;
            transform.position += speed * Time.deltaTime * interactingCharacterDirection;
            yield return null;
        }

        enemyAnimator.StunToIdleAnimation();

        // Stay in place for a while
        while (timer > .0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        characterMovement.CanMove = true;
        health.IsInvincible = false;
        isStunned = false;
        isStaggered = false;
        yield return null;
    }
}
