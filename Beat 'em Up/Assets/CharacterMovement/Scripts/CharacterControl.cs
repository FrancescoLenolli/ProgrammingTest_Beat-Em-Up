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
        [HideInInspector]
        public float otherCharacterPositionX = 0.0f;

        protected CharacterInput characterInput;
        private Rigidbody rb;
        private Rigidbody2D rb2D;

        private void Start()
        {
            SetUp();
        }

        protected virtual void SetUp()
        {
            GetComponents();

            characterMovement.SetUp(rb, rb2D, movementValues);
        }

        protected virtual void GetComponents()
        {
            characterMovement = CharacterUtilities.TryGetComponent<CharacterMovement>(gameObject);

            if (movementValues.isCharacterBidimensional)
                rb2D = CharacterUtilities.TryGetComponent<Rigidbody2D>(gameObject);
            else
                rb = CharacterUtilities.TryGetComponent<Rigidbody>(gameObject);

        }

        protected void BounceBack()
        {
            StartCoroutine(BounceBackRoutine(.2f));
        }

        private IEnumerator BounceBackRoutine(float time)
        {
            float timer = time;
            float speed = 1.0f;
            characterMovement.canMove = false;
            Vector3 moveInput = transform.position.x > otherCharacterPositionX ? Vector3.right : Vector3.left;

            while (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                transform.position += speed * Time.deltaTime * moveInput;
                yield return null;
            }

            characterMovement.canMove = true;
            yield return null;
        }
    }
}
