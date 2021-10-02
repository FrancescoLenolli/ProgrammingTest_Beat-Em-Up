using UnityEngine;

namespace CoreCharacter
{
    public enum InputType { XZAxis, XYAxis, XAxis }

    /// <summary>
    /// Store base Movement values.
    /// </summary>
    [CreateAssetMenu(menuName = "MovementValues", fileName = "NewValues")]
    public class MovementValues : ScriptableObject
    {
        [Tooltip("On which axes does the character move.")]
        public InputType inputType = InputType.XZAxis;
        [Tooltip("When set to TRUE the character will use the Transform component to move instead of the rigidbody.")]
        public bool isCharacterBidimensional = false;
        [Tooltip("Base movement speed.")]
        public float speed = 1.0f;
        [Tooltip("Base jump force.")]
        public float jumpForce = 1.0f;
    }
}
