using CoreCharacter.Utilities;
using System.Collections;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField]
        protected MovementValues movementValues = null;

        [HideInInspector]
        public CharacterMovement characterMovement = null;
        // Direction of another character interacting wiht this one.
        [HideInInspector]
        public Vector3 interactingCharacterDirection;

        protected CharacterInput characterInput;
        protected bool isStaggered = false;

        public bool IsStaggered { get => isStaggered; set => isStaggered = value; }

        private void Start()
        {
            SetUp();
        }

        protected virtual void SetUp()
        {
            GetComponents();

            characterMovement.SetUp(movementValues);
        }

        protected virtual void GetComponents()
        {
            characterMovement = CharacterUtilities.TryGetComponent<CharacterMovement>(gameObject);
        }

        /// <summary>
        /// Move Character back for x amount of seconds.
        /// </summary>
        protected void BounceBack(float bounceBackTime)
        {
            if (!isStaggered)
                StartCoroutine(BounceBackRoutine(bounceBackTime));
        }

        private IEnumerator BounceBackRoutine(float time)
        {
            isStaggered = true;

            float timer = time;
            float speed = 1.0f;
            characterMovement.CanMove = false;

            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                transform.position += speed * Time.deltaTime * interactingCharacterDirection;
                yield return null;
            }

            characterMovement.CanMove = true;
            isStaggered = false;
            yield return null;
        }
    }
}
