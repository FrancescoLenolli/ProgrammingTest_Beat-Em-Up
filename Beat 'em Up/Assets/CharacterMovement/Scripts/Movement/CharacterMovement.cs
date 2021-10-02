using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterMovement : MonoBehaviour
    {
        [HideInInspector]
        public bool canMove = true;

        private MovementValues movementValues;
        private MovementType movementType;
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

        public void SetUp(MovementValues movementValues)
        {
            this.movementValues = movementValues;
            moveInputValue = Vector3.zero;
            isJumping = false;

            if (movementValues.isCharacterBidimensional)
            {
                rb2D = CharacterUtilities.TryGetComponent<Rigidbody2D>(gameObject);
                movementType = new TwoDimensionMovement(rb2D, transform, movementValues);
            }
            else
            {
                rb = CharacterUtilities.TryGetComponent<Rigidbody>(gameObject);
                movementType = new ThreeDimensionMovement(rb, transform, movementValues, out isjumpEnabled);
            }
        }

        public void HandleMovement(Vector3 movementValue)
        {
            if (!canMove)
                return;

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
            movementType.Move(moveInputValue);
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
