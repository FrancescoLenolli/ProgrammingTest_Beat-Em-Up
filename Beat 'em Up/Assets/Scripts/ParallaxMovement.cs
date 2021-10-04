using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxMovement : MonoBehaviour
{
    [Tooltip("Strength of the Parallax effect.\n" +
        "The more the value is close to 1, the weaker the effect will be.\n")]
    [Range(.1f, 1.0f)]
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
        ParallaxEffect();
    }

    private void ParallaxEffect()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        // deltaMovement.y * effectMultiplier to have the parallax effect on the y axis.
        transform.position += new Vector3(deltaMovement.x * effectMultiplier, 0, 0);
        lastCameraPosition = cameraTransform.position;
    }
}
