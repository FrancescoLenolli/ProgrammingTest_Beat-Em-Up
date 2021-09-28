using System;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterInput : MonoBehaviour
    {
        private Vector2 moveInput = Vector2.zero;
        private Vector2 rotateInput = Vector2.zero;
        private bool jumpCheck = true;
        private Action<Vector3> actionMove;
        private Action<Vector2> actionRotate;
        private Action actionJump;

        public Action<Vector3> ActionMove { get => actionMove; set => actionMove = value; }
        public Action<Vector2> ActionRotate { get => actionRotate; set => actionRotate = value; }
        public Action ActionJump { get => actionJump; set => actionJump = value; }

        private void Update()
        {
            HandleMove();
            HandleRotate();
            HandleJump();
        }

        private void HandleMove()
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");

            actionMove?.Invoke(new Vector3(moveInput.x, moveInput.y, 0));
        }

        private void HandleRotate()
        {
            actionRotate?.Invoke(rotateInput);
        }

        private void HandleJump()
        {
            if (Input.GetAxis("Jump") == 1 && jumpCheck)
            {
                actionJump?.Invoke();
                jumpCheck = false;
            }
            if (Input.GetAxis("Jump") == 0 && !jumpCheck)
            {
                jumpCheck = true;
            }
        }
    }
}
