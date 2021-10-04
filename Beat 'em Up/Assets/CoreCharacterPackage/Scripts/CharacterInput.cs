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
            HandleAction(Input.GetAxis("Jump"), jumpCheck, actionJump, out jumpCheck);
            HandleAction(Input.GetAxis("Fire3"), action1Check, action1, out action1Check);
            HandleAction(Input.GetAxis("Fire2"), action2Check, action2, out action2Check);
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

        private void HandleAction(float axisValue, bool check, Action action, out bool checkValue)
        {
            /* The input sent when pressing the axis's button is continuously sent.
             * I need to have some checks to have them behave like a standard on/off button.
             */

            bool checkResult = true;

            if (axisValue == 1 && check)
            {
                action?.Invoke();
                checkResult = false;
            }
            if (axisValue == 0 && !check)
            {
                checkResult = true;
            }

            checkValue = checkResult;
        }
    }
}
