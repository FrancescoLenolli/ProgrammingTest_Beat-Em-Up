using UnityEngine;

namespace CoreCharacter
{
    public enum InputType { XZAxis, XYAxis, XAxis }

    [CreateAssetMenu(menuName = "MovementValues", fileName = "NewValues")]
    public class MovementValues : ScriptableObject
    {
        public InputType inputType = InputType.XZAxis;
        public float speed = 1.0f;
        public float jumpForce = 1.0f;
    }
}
