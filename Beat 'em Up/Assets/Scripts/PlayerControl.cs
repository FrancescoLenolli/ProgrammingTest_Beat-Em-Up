using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class PlayerControl : CharacterControl
{
    private Animator animator;
    private CharacterAnimator characterAnimator;
    private CharacterAttack characterAttack;

    protected override void SetUp()
    {
        base.SetUp();
        characterAnimator.SetUp(animator);
        characterAttack.SetUp(characterAnimator);
        characterInput.ActionMove += characterAnimator.HandleAnimation;
        characterInput.ActionAttack += characterAttack.StartAttack;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
        characterAttack = CharacterUtilities.TryGetComponent<CharacterAttack>(gameObject);
    }
}
