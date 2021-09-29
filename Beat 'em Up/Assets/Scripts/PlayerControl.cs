﻿using CoreCharacter;
using CoreCharacter.Utilities;
using UnityEngine;

public class PlayerControl : CharacterControl
{
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

        characterInput.ActionMove += characterMovement.HandleMovement;
        characterInput.ActionJump += characterMovement.SetJump;
        characterInput.ActionMove += characterAnimator.HandleAnimation;
        characterInput.ActionAttack += characterAttack.StartAttack;
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = GetComponent<Animator>();
        health = GetComponent<HealthComponent>();
        characterInput = CharacterUtilities.TryGetComponent<CharacterInput>(gameObject);
        characterAnimator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
        characterAttack = CharacterUtilities.TryGetComponent<CharacterAttack>(gameObject);
    }
}
