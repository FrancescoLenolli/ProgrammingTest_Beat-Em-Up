using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform lowerLimit;
    public Transform upperLimit;
    public Transform targetTransform;

    private Camera mainCamera;
    private Vector2 positionXLimit;
    private Vector2 positionYLimit;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        SetCameraLimits(lowerLimit.position, upperLimit.position);
    }

    private void LateUpdate()
    {
        float positionX = Mathf.Clamp(targetTransform.position.x, positionXLimit.x, positionXLimit.y);
        float positionY = Mathf.Clamp(targetTransform.position.y, positionYLimit.x, positionYLimit.y);
        transform.position = new Vector3(positionX, positionY, transform.position.z);
    }

    private void SetCameraLimits(Vector3 lowLimit, Vector3 highLimit)
    {
        float cameraSize = mainCamera.orthographicSize;

        positionXLimit = new Vector2(lowLimit.x + (cameraSize*2), highLimit.x - (cameraSize*2));
        positionYLimit = new Vector2(lowLimit.y + cameraSize, highLimit.y - cameraSize);
    }
}
