using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Transform targetTransform;
    private Vector2 positionXLimit;
    private Vector2 positionYLimit;
    private bool isCameraLocked;

    private void LateUpdate()
    {
        if (!targetTransform || isCameraLocked)
            return;

        float positionX = Mathf.Clamp(targetTransform.position.x, positionXLimit.x, positionXLimit.y);
        float positionY = Mathf.Clamp(targetTransform.position.y, positionYLimit.x, positionYLimit.y);
        transform.position = new Vector3(positionX, positionY, transform.position.z);
    }

    public void SetUp(Transform lowerLimit, Transform upperLimit, Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        mainCamera = GetComponent<Camera>();
        SetCameraLimits(lowerLimit.position, upperLimit.position);
    }

    public void LockCamera(bool lockCamera)
    {
        isCameraLocked = lockCamera;
    }

    public bool IsCameraLocked()
    {
        return isCameraLocked;
    }

    public Vector3 GetRightLimit()
    {
        float cameraHorizontalSize = mainCamera.orthographicSize * 2;
        return new Vector3(transform.position.x + cameraHorizontalSize, transform.position.y, 0);
    }

    public Vector3 GetLeftLimit()
    {
        float cameraHorizontalSize = mainCamera.orthographicSize * 2;
        return new Vector3(transform.position.x - cameraHorizontalSize, transform.position.y, 0);
    }


    private void SetCameraLimits(Vector3 lowLimit, Vector3 highLimit)
    {
        float cameraSize = mainCamera.orthographicSize;

        positionXLimit = new Vector2(lowLimit.x + (cameraSize*2), highLimit.x - (cameraSize*2));
        positionYLimit = new Vector2(lowLimit.y + cameraSize, highLimit.y - cameraSize);
    }
}
