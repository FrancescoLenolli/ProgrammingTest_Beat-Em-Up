using System;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterInput : MonoBehaviour
    {
        private Vector2 moveInput = Vector2.zero;
        private Vector2 rotateInput = Vector2.zero;
        private bool jumpCheck = true;
        private bool attack1Check = true;
        private bool attack2Check = true;
        private Action<Vector3> actionMove;
        private Action<Vector2> actionRotate;
        private Action actionJump;
        private Action actionAttack1;
        private Action actionAttack2;

        public Action<Vector3> ActionMove { get => actionMove; set => actionMove = value; }
        public Action<Vector2> ActionRotate { get => actionRotate; set => actionRotate = value; }
        public Action ActionJump { get => actionJump; set => actionJump = value; }
        public Action ActionAttack1 { get => actionAttack1; set => actionAttack1 = value; }
        public Action ActionAttack2 { get => actionAttack2; set => actionAttack2 = value; }

        private void Update()
        {
            HandleMove();
            HandleRotate();
            HandleJump();
            HandleAttack1();
            HandleAttack2();
        }

        private void HandleAttack1()
        {
            if (Input.GetAxis("Fire3") == 1 && attack1Check)
            {
                ActionAttack1?.Invoke();
                attack1Check = false;
            }
            if (Input.GetAxis("Fire3") == 0 && !attack1Check)
            {
                attack1Check = true;
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                ActionAttack1?.Invoke();
                attack1Check = false;
            }
        }

        private void HandleAttack2()
        {
            if (Input.GetAxis("Fire2") == 1 && attack2Check)
            {
                ActionAttack2?.Invoke();
                attack2Check = false;
            }
            if (Input.GetAxis("Fire2") == 0 && !attack2Check)
            {
                attack2Check = true;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                ActionAttack2?.Invoke();
                attack2Check = false;
            }
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
