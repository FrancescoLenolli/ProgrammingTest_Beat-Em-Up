using UnityEngine;

namespace CoreCharacter
{
    public enum InputType { XZAxis, XYAxis, XAxis }

    [CreateAssetMenu(menuName = "MovementValues", fileName = "NewValues")]
    public class MovementValues : ScriptableObject
    {
        public InputType inputType = InputType.XZAxis;
        [Tooltip("When set to TRUE the character will use the Transform component to move instead of the rigidbody.")]
        public bool isCharacterBidimensional = false;
        public float speed = 1.0f;
        public float jumpForce = 1.0f;
    }
}
