using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class EnemyControl : CharacterControl
{
    [SerializeField]
    private float healthValue = 1.0f;

    private Animator animator;
    private CharacterAnimator characterAnimator;
    private CharacterAttack attackComponent;
    private PlayerControl target;
    private HealthComponent health;
    private LevelManager levelManager;

    public HealthComponent Health { get => health; set => health = value; }
    public PlayerControl Target { get => target; }
    public CharacterAttack AttackComponent { get => attackComponent; }
    public CharacterAnimator CharacterAnimator { get => characterAnimator; }


    public void Init(LevelManager levelManager, PlayerControl target, Vector3 startingPosition)
    {
        this.levelManager = levelManager;
        this.target = target;
        transform.position = startingPosition;
        SetUp();
        health.OnHealthDepleted += levelManager.EnemyDied;
    }

    protected override void SetUp()
    {
        base.SetUp();
        characterAnimator.SetUp(animator);
        attackComponent.SetUp(characterAnimator);
        health.Value = healthValue;
        health.OnHealthDepleted += Die;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        health = CharacterUtilities.TryGetComponent<HealthComponent>(gameObject);
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
        attackComponent = CharacterUtilities.TryGetComponent<EnemyAttack>(gameObject);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
