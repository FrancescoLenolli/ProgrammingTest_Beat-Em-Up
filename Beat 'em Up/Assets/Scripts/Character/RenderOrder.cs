using UnityEngine;

public class RenderOrder : MonoBehaviour
{
    // I need this to counter the origin point of the characters. A properly made asset doesn't need this.
    [Tooltip("Represent the half height of the parent object.")]
    [SerializeField]
    private float transformHalfHeight = 0.3f;

    private SpriteRenderer spriteRenderer;
    private float halfHeight;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        halfHeight = transformHalfHeight;
    }

    private void Update()
    {
        int sortingOrder = (int)(-(transform.position.y - halfHeight) * 10);
        spriteRenderer.sortingOrder = sortingOrder;
    }

    // Origin point of the character is different when standing up or laying down.
    // If the character is laying dead, the half height is not needed to calculate the sorting order.
    public void ResetHalfHeight()
    {
        halfHeight = halfHeight == 0 ? transformHalfHeight : 0;
    }
}
