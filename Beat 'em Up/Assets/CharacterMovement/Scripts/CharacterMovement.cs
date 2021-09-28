using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterMovement : MonoBehaviour
    {
        private MovementValues movementValues;
        private Rigidbody rb;
        private Rigidbody2D rb2D;
        private Vector3 moveInputValue;
        private bool canJump;
        private bool isJumping;
        private bool isjumpEnabled = true;
        private bool isGrounded;

        private void FixedUpdate()
        {
            if (!movementValues.isCharacterBidimensional)
                isGrounded = CharacterUtilities.IsGrounded(rb.transform);

            if (!isjumpEnabled)
                return;

            HandleJump();
        }

        public void SetUp(Rigidbody rigidbody, Rigidbody2D rigidbody2D, MovementValues movementValues)
        {
            this.movementValues = movementValues;
            moveInputValue = Vector2.zero;
            isJumping = false;

            if (movementValues.isCharacterBidimensional)
            {
                rb2D = rigidbody2D;
                if (movementValues.inputType == InputType.XYAxis)
                {
                    rb2D.gravityScale = 0;
                }
            }
            else
            {
                rb = rigidbody;
                if (movementValues.inputType == InputType.XYAxis)
                {
                    if (rb)
                        rb.isKinematic = true;
                    isjumpEnabled = false;
                }
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

            if (movementValues.isCharacterBidimensional && rb2D)
            {
                rb2D.MovePosition(new Vector2(newPosition.x, newPosition.y));

                if (moveInputValue != Vector3.zero)
                    transform.rotation = moveInputValue.x < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
            }
            else if (rb)
            {
                rb.MovePosition(newPosition);
            }

        }

        private void HandleJump()
        {
            bool alreadyJumped = isJumping && isGrounded && rb.useGravity;

            if (canJump)
            {
                Jump();
            }
            else if (alreadyJumped)
            {
                isJumping = false;
            }

            if (rb)
                rb.useGravity = !isGrounded;
        }

        private void Jump()
        {
            rb.AddForce(Vector3.up * movementValues.jumpForce, ForceMode.Impulse);
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
