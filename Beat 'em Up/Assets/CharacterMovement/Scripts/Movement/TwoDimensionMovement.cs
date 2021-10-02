using CoreCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionMovement : MovementType
{
    public TwoDimensionMovement(Rigidbody2D rb2D, Transform transform, MovementValues movementValues)
    {
        this.rb2D = rb2D;
        this.transform = transform;
        this.movementValues = movementValues;

        if (movementValues.inputType == InputType.XYAxis)
        {
            rb2D.gravityScale = 0;
            rb2D.freezeRotation = true;
        }
    }

    public override void Move(Vector3 moveInput)
    {
        Vector3 velocity = movementValues.speed * Time.fixedDeltaTime * moveInput;
        Vector3 newPosition = transform.position + velocity;

        rb2D.MovePosition(new Vector2(newPosition.x, newPosition.y));

        if (moveInput != Vector3.zero)
            transform.rotation = moveInput.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
    }
}
