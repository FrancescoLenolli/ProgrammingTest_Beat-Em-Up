using CoreCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementType
{
    protected Rigidbody rb;
    protected Rigidbody2D rb2D;
    protected Transform transform;
    protected MovementValues movementValues;

    public virtual void Move(Vector3 moveInput) { }
}
