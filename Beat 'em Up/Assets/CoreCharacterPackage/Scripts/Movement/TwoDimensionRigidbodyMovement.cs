using UnityEngine;

namespace CoreCharacter
{
    public class TwoDimensionRigidbodyMovement : MovementType
    {
        /// <summary>
        /// Move character using a Rigidbody2D Component.
        /// </summary>
        public TwoDimensionRigidbodyMovement(Rigidbody2D rb2D, Transform transform, MovementValues movementValues, out bool canJump)
        {
            this.rb2D = rb2D;
            this.transform = transform;
            this.movementValues = movementValues;
            bool jumpEnabled = true;

            if (movementValues.inputType == InputType.XYAxis)
            {
                rb2D.gravityScale = 0;
                rb2D.freezeRotation = true;
                jumpEnabled = false;
            }

            canJump = jumpEnabled;
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
}
