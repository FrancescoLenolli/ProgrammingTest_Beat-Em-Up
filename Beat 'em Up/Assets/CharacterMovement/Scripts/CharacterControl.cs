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

        private void Start()
        {
            SetUp();
        }

        private void SetUp()
        {
            GetComponents();

            characterMovement.SetUp(rb, movementValues);

            characterInput.ActionMove += characterMovement.HandleMovement;
            characterInput.ActionJump += characterMovement.SetJump;
        }

        private void GetComponents()
        {
            characterInput = CharacterUtilities.TryGetComponent<CharacterInput>(gameObject);
            characterMovement = CharacterUtilities.TryGetComponent<CharacterMovement>(gameObject);
            rb = CharacterUtilities.TryGetComponent<Rigidbody>(gameObject);
        }
    }
}
