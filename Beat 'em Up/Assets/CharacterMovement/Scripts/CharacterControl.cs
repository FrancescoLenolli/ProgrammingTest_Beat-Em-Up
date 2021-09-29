using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField]
        protected MovementValues movementValues = null;

        [HideInInspector]
        public CharacterMovement characterMovement = null;

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
    }
}
