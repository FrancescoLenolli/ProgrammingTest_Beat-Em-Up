using CoreCharacter;
using UnityEngine;

public class ThreeDimensionMovement : MovementType
{
    public ThreeDimensionMovement(Rigidbody rb, Transform transform, MovementValues movementValues, out bool isJumpEnabled)
    {
        this.rb = rb;
        this.movementValues = movementValues;
        this.transform = transform;
        isJumpEnabled = true;

        if (movementValues.inputType == InputType.XYAxis)
        {
            if (rb)
                rb.isKinematic = true;
            isJumpEnabled = false;
        }
    }

    public override void Move(Vector3 moveInput)
    {
        Vector3 velocity = movementValues.speed * Time.fixedDeltaTime * moveInput;
        Vector3 newPosition = transform.position + velocity;

        rb.MovePosition(newPosition);
    }
}
