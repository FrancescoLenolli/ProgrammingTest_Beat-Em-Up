using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterMovement : MonoBehaviour
    {
        private MovementValues movementValues;
        private MovementType movementType;
        private Rigidbody rb;
        private Rigidbody2D rb2D;
        private Vector3 moveInputValue;
        private bool canMove;
        private bool canJump;
        private bool isJumping;
        private bool isGrounded;

        public bool CanMove { get => canMove; set => canMove = value; }

        private void FixedUpdate()
        {
            if (!movementValues.isCharacterBidimensional)
                isGrounded = CharacterUtilities.IsGrounded(rb.transform);

            if (!canJump)
                return;

            HandleJump();
        }

        public void SetUp(MovementValues movementValues)
        {
            this.movementValues = movementValues;
            moveInputValue = Vector3.zero;
            canMove = true;
            canJump = true;
            isJumping = false;

            if (movementValues.isCharacterBidimensional)
            {
                rb2D = CharacterUtilities.TryGetComponent<Rigidbody2D>(gameObject);
                movementType = new TwoDimensionRigidbodyMovement(rb2D, transform, movementValues, out canJump);
            }
            else
            {
                rb = CharacterUtilities.TryGetComponent<Rigidbody>(gameObject);
                movementType = new ThreeDimensionRigidbodyMovement(rb, transform, movementValues, out canJump);
            }
        }

        public void HandleMovement(Vector3 movementValue)
        {
            if (!canMove)
                return;

            moveInputValue = ModifyInput(movementValue);
            HandlePosition(moveInputValue);
        }

        /// <summary>
        /// Rotate to face the moement direction.
        /// </summary>
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

        /// <summary>
        /// Modify input received according to the InputType selected on the MovementValue used.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
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
