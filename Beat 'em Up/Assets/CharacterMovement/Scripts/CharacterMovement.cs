using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterMovement : MonoBehaviour
    {
        private MovementValues movementValues;
        private new Rigidbody rigidbody;
        private Vector3 moveInputValue;
        private bool canJump;
        private bool isJumping;
        private bool isjumpEnabled = true;
        private bool isGrounded;

        private void FixedUpdate()
        {
            isGrounded = CharacterUtilities.IsGrounded(rigidbody.transform);

            if (!isjumpEnabled)
                return;

            HandleJump();
        }

        public void SetUp(Rigidbody rigidbody, MovementValues movementValues)
        {
            this.rigidbody = rigidbody;
            this.movementValues = movementValues;
            moveInputValue = Vector2.zero;
            isJumping = false;

            if (movementValues.inputType == InputType.XYAxis)
            {
                rigidbody.isKinematic = true;
                isjumpEnabled = false;
            }
        }

        public void HandleMovement(Vector3 movementValue)
        {
            moveInputValue = ModifyInput(movementValue);
            HandlePosition(moveInputValue);
        }

        public void HandleRotation()
        {
            if (moveInputValue == Vector3.zero)
                return;

            Vector3 lookAtPosition = new Vector3(moveInputValue.x, 0.0f, moveInputValue.z);
            Quaternion currentRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition);

            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 1.0f);
        }

        public void SetJump()
        {
            canJump = CharacterUtilities.IsGrounded(transform);
        }

        private void HandlePosition(Vector3 moveInputValue)
        {
            Vector3 velocity = movementValues.speed * Time.fixedDeltaTime * moveInputValue;
            Vector3 newPosition = transform.position + velocity;

            rigidbody.MovePosition(newPosition);
        }

        private void HandleJump()
        {
            bool alreadyJumped = isJumping && isGrounded && rigidbody.useGravity;

            if (canJump)
            {
                Jump();
            }
            else if (alreadyJumped)
            {
                isJumping = false;
            }

            rigidbody.useGravity = !isGrounded;
        }

        private void Jump()
        {
            rigidbody.AddForce(Vector3.up * movementValues.jumpForce, ForceMode.Impulse);
            isJumping = true;
            canJump = false;
        }

        private Vector3 ModifyInput(Vector3 input)
        {
            Vector3 result = Vector3.zero;

            switch (movementValues.inputType)
            {
                case InputType.XZAxis:
                    result = new Vector3(input.x, 0, input.y);
                    break;
                case InputType.XYAxis:
                    result = input;
                    break;
                case InputType.XAxis:
                    result = new Vector3(input.x, 0, 0);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
