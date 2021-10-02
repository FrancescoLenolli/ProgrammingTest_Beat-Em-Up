using System;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterInput : MonoBehaviour
    {
        private Vector2 moveInput = Vector2.zero;
        private Vector2 rotateInput = Vector2.zero;
        private bool jumpCheck = true;
        private bool action1Check = true;
        private bool action2Check = true;
        private Action<Vector3> actionMove;
        private Action<Vector2> actionRotate;
        private Action actionJump;
        private Action action1;
        private Action action2;

        public Action<Vector3> ActionMove { get => actionMove; set => actionMove = value; }
        public Action<Vector2> ActionRotate { get => actionRotate; set => actionRotate = value; }
        public Action ActionJump { get => actionJump; set => actionJump = value; }
        public Action Action1 { get => action1; set => action1 = value; }
        public Action Action2 { get => action2; set => action2 = value; }

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
            if (Input.GetAxis("Fire3") == 1 && action1Check)
            {
                Action1?.Invoke();
                action1Check = false;
            }
            if (Input.GetAxis("Fire3") == 0 && !action1Check)
            {
                action1Check = true;
            }

            if(Input.GetKeyDown(KeyCode.K))
            {
                Action1?.Invoke();
                action1Check = false;
            }
        }

        private void HandleAttack2()
        {
            if (Input.GetAxis("Fire2") == 1 && action2Check)
            {
                Action2?.Invoke();
                action2Check = false;
            }
            if (Input.GetAxis("Fire2") == 0 && !action2Check)
            {
                action2Check = true;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                Action2?.Invoke();
                action2Check = false;
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
