using UnityEngine;

namespace CoreCharacter
{
    /// <summary>
    /// Base class used to move Characters.
    /// </summary>
    public class MovementType
    {
        protected Rigidbody rb;
        protected Rigidbody2D rb2D;
        protected Transform transform;
        protected MovementValues movementValues;

        public virtual void Move(Vector3 moveInput) { }
    }
}
