using UnityEngine;

namespace CoreCharacter
{
    public class ThreeDimensionRigidbodyMovement : MovementType
    {
        /// <summary>
        /// Move Character using a Rigidbody Component.
        /// </summary>
        public ThreeDimensionRigidbodyMovement(Rigidbody rb, Transform transform, MovementValues movementValues, out bool isJumpEnabled)
        {
            this.rb = rb;
            this.movementValues = movementValues;
            this.transform = transform;
            isJumpEnabled = true;

            // XY Axis with a 3D rigidbody is intended for Top-Down games.
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
}
