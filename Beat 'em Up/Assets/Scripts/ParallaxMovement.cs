using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    [SerializeField]
    private float effectMultiplier = .5f;

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    private void Awake()
    {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * effectMultiplier, 0, 0);
        lastCameraPosition = cameraTransform.position;
    }
}
