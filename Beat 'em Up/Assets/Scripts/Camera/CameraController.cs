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
    private CinemachineVirtualCamera virtualCamera;

    private void Awake()
    {
        if (debug)
            DebugSetUp();
    }

    public void SetUp(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
        mainCamera = Camera.main;
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = targetTransform;
    }

    /// <summary>
    /// If set to TRUE, stop the camera in his current position.
    /// </summary>
    /// <param name="lockCamera"></param>
    public void LockCamera(bool lockCamera)
    {
        virtualCamera.Follow = lockCamera ? null : targetTransform;
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

    // Set up the camera without the need of a LevelManager
    private void DebugSetUp()
    {
        mainCamera = Camera.main;
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        targetTransform = FindObjectOfType<PlayerControl>().transform;
    }
}
