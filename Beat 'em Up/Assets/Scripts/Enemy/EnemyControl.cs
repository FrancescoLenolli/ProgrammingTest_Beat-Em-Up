using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class EnemyControl : CharacterControl
{
    private Animator animator;
    private CharacterAnimator characterAnimator;
    private CharacterAttack characterAttack;
    private PlayerControl target;
    private HealthComponent health;

    public HealthComponent Health { get => health; set => health = value; }
    public PlayerControl Target { get => target; set => target = value; }
    public CharacterAttack CharacterAttack { get => characterAttack; set => characterAttack = value; }
    public CharacterAnimator CharacterAnimator { get => characterAnimator; set => characterAnimator = value; }

    protected override void SetUp()
    {
        base.SetUp();
        characterAnimator.SetUp(animator);
        characterAttack.SetUp(characterAnimator);
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthComponent>();
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
        characterAttack = CharacterUtilities.TryGetComponent<CharacterAttack>(gameObject);
        target = FindObjectOfType<PlayerControl>();
    }
}
