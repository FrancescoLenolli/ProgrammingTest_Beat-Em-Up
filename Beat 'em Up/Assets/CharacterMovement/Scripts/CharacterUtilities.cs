using UnityEngine;

namespace CoreCharacter.Utilities
{
    public static class CharacterUtilities
    {
        /// <summary>
        /// Returns TRUE if the given Transform is touching the ground.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static bool IsGrounded(Transform transform)
        {
            bool isGrounded;
            float offset = 0.1f;
            Vector3 startPoint = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);
            Physics.Raycast(startPoint, Vector3.down, out RaycastHit hitInfo, 0.1f + offset);
            Debug.DrawRay(startPoint, Vector3.down, Color.red);
            if (!hitInfo.transform)
                isGrounded = false;
            else
                isGrounded = hitInfo.transform != transform;

            return isGrounded;
        }

        /// <summary>
        /// Returns TRUE if the given rigidbody is falling down.
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <returns></returns>
        public static bool IsFalling(Rigidbody rigidbody)
        {
            if (!rigidbody)
            {
                Debug.Log($"{rigidbody.name} missing Rigidbody component");
                return false;
            }

            return rigidbody.velocity.y < 0.0f;
        }

        /// <summary>
        /// Return squared distance between two objects.
        /// Avoid costly squared calculation.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static float SqrDistance(Transform character, Transform target)
        {
            Vector3 offset = target.position - character.position;

            return offset.sqrMagnitude;
        }

        /// <summary>
        /// Return true if the target is within a certain range from the character.
        /// </summary>
        /// <param name="character"></param>
        /// <param name="target"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public static bool IsTargetInRange(Transform character, Transform target, float range)
        {
            Vector3 offset = target.position - character.position;

            return offset.sqrMagnitude < range * range;
        }

        /// <summary>
        /// Try to get a gameobject component, Add it on the fly if missing.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static T TryGetComponent<T>(GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();

            if (!component)
                component = gameObject.AddComponent<T>();

            return component;
        }
    }
}
