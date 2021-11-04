using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Set to TRUE if you're testing the project.\nThe camera doesn't need a LevelManager to work.\n" +
        "Be sure to have two objects in the scene named 'UpperLimit' and 'LowerLimit'.\n" +
        "They will be used to limit the camera position to a certain area.")]
    [SerializeField]
    private bool debug = false;

    private Camera mainCamera;
    private Transform targetTransform;
    private Vector2 positionXLimit;
    private Vector2 positionYLimit;
    private bool isCameraLocked;
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (debug)
            DebugSetUp();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = FindObjectOfType<PlayerControl>().transform;
    }

    private void LateUpdate()
    {
        if (!targetTransform || isCameraLocked)
            return;

        //MoveCamera();
    }

    public void SetUp(Transform lowerLimit, Transform upperLimit, Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        mainCamera = GetComponent<Camera>();
        SetCameraLimits(lowerLimit.position, upperLimit.position);
    }

    /// <summary>
    /// If set to TRUE, stop the camera in his current position.
    /// </summary>
    /// <param name="lockCamera"></param>
    public void LockCamera(bool lockCamera)
    {
        //isCameraLocked = lockCamera;
        virtualCamera.Follow = lockCamera ? null : FindObjectOfType<PlayerControl>().transform;
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

    /// <summary>
    /// Camera should not move outside these limits.
    /// </summary>
    /// <param name="lowLimit"></param> The bottom left of the current Level.
    /// <param name="highLimit"></param> The top right of the current Level.
    private void SetCameraLimits(Vector3 lowLimit, Vector3 highLimit)
    {
        // OrthographicSize = height of a camera with an Ortographic viewport.
        float cameraSize = mainCamera.orthographicSize;

        positionXLimit = new Vector2(lowLimit.x + (cameraSize * 2), highLimit.x - (cameraSize * 2));
        positionYLimit = new Vector2(lowLimit.y + cameraSize, highLimit.y - cameraSize);
    }

    private void MoveCamera()
    {
        float positionX = Mathf.Clamp(targetTransform.position.x, positionXLimit.x, positionXLimit.y);
        float positionY = Mathf.Clamp(targetTransform.position.y, positionYLimit.x, positionYLimit.y);

        transform.position = new Vector3(positionX, positionY, transform.position.z);
    }

    private void DebugSetUp()
    {
        mainCamera = GetComponent<Camera>();
        Transform lowerLimit = GameObject.Find("LowerLimit").transform;
        Transform upperLimit = GameObject.Find("UpperLimit").transform;
        SetCameraLimits(lowerLimit.position, upperLimit.position);
        targetTransform = FindObjectOfType<PlayerControl>().transform;
        isCameraLocked = false;
    }
}
