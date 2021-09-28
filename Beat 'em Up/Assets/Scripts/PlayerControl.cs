using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class PlayerControl : CharacterControl
{
    private Animator animator;
    private CharacterAnimator characterAnimator;

    protected override void SetUp()
    {
        base.SetUp();
        characterAnimator.SetUp(animator);
        characterInput.ActionMove += characterAnimator.HandleAnimation;
        characterInput.ActionAttack += characterAnimator.AttackAnimation;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
    }
}
