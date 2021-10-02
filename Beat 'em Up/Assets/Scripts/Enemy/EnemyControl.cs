using CoreCharacter;
using CoreCharacter.Utilities;
using System.Collections;
using UnityEngine;

public class EnemyControl : CharacterControl
{
    [Tooltip("Set to TRUE if you're debugging the enemy behaviours.\nDoesn't need a LevelManager to work.")]
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
    private CharacterAnimator characterAnimator;
    private PlayerControl target;
    private HealthComponent health;
    private StateMachine stateMachine;

    public HealthComponent Health { get => health; set => health = value; }
    public PlayerControl Target { get => target; }
    public CharacterAttack AttackNormal { get => attackNormal; }
    public CharacterAttack AttackHeavy { get => attackHeavy; set => attackHeavy = value; }
    public CharacterAnimator CharacterAnimator { get => characterAnimator; }

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

    protected override void SetUp()
    {
        base.SetUp();
        attackNormal.ResetTimer();
        attackHeavy.ResetTimer();
        characterAnimator.SetUp(animator);
        attackNormal.OnAttack += characterAnimator.AttackAnimation;
        attackHeavy.OnAttack += characterAnimator.AttackHeavyAnimation;
        health.Set(healthValue);
        health.OnDamageReceived += characterAnimator.HitAnimation;
        health.OnDamageReceived += Stagger;
        health.OnHealthDepleted += Die;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        health = CharacterUtilities.TryGetComponent<HealthComponent>(gameObject);
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
        stateMachine = GetComponent<StateMachine>();
    }

    private void Stagger()
    {
        BounceBack(staggerTime);
    }

    private void Die()
    {
        stateMachine.Stop();
        characterAnimator.DeathAnimation();
    }
}
