using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class PlayerControl : CharacterControl
{
    [SerializeField]
    private float healthValue = 2.0f;

    private Animator animator;
    private CharacterAnimator characterAnimator;
    private CharacterAttack characterAttack;
    private HealthComponent health;

    public HealthComponent Health { get => health; set => health = value; }

    protected override void SetUp()
    {
        base.SetUp();
        characterAnimator.SetUp(animator);
        characterAttack.SetUp(characterAnimator);
        health.Value = healthValue;

        characterInput.ActionMove += characterMovement.HandleMovement;
        characterInput.ActionJump += characterMovement.SetJump;
        characterInput.ActionMove += characterAnimator.HandleAnimation;
        characterInput.ActionAttack += characterAttack.StartAttack;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        health = CharacterUtilities.TryGetComponent<HealthComponent>(gameObject);
        characterInput = CharacterUtilities.TryGetComponent<CharacterInput>(gameObject);
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
        characterAttack = CharacterUtilities.TryGetComponent<PlayerAttack>(gameObject);
    }
}
