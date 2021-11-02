using UnityEngine;

public class RenderOrder : MonoBehaviour
{
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

    public void ResetHalfHeight()
    {
        halfHeight = halfHeight == 0 ? transformHalfHeight : 0;
    }
}
