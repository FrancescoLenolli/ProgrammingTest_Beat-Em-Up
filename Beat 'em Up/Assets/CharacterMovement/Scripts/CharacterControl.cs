using CoreCharacter.Utilities;
using UnityEngine;

namespace CoreCharacter
{
    public class CharacterControl : MonoBehaviour
    {
        [SerializeField]
        private MovementValues movementValues = null;
        private CharacterInput characterInput;
        private CharacterMovement characterMovement;
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

            characterInput.ActionMove += characterMovement.HandleMovement;
            characterInput.ActionJump += characterMovement.SetJump;
        }

        protected virtual void GetComponents()
        {
            characterInput = CharacterUtilities.TryGetComponent<CharacterInput>(gameObject);
            characterMovement = CharacterUtilities.TryGetComponent<CharacterMovement>(gameObject);

            if (movementValues.isCharacterBidimensional)
                rb2D = CharacterUtilities.TryGetComponent<Rigidbody2D>(gameObject);
            else
                rb = CharacterUtilities.TryGetComponent<Rigidbody>(gameObject);

        }
    }
}
