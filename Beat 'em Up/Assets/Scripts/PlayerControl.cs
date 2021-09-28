using CoreCharacter;
using CoreCharacter.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : CharacterControl
{
    private CharacterAnimator animator;

    protected override void SetUp()
    {
        base.SetUp();
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        animator = CharacterUtilities.TryGetComponent<CharacterAnimator>(gameObject);
    }
}
