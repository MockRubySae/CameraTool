using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    public float radius = 10f;
    private CameraController cameraController;

    private void Start()
    {
        cameraController = GetComponent<CameraController>();
    }

    private void LateUpdate()
    {
        Vector3 directionToCamera = transform.position - cameraController.player.position;
        float currentDistance = directionToCamera.magnitude;

        if (currentDistance > radius)
        {
            Vector3 clampedPosition = cameraController.player.position + (directionToCamera.normalized * radius);
            transform.position = clampedPosition;
        }
    }
    private void OnDrawGizmos()
    {
        if (cameraController != null && cameraController.player != null)
        {
            // Draw boundary sphere
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(cameraController.player.position, radius);

            // Draw line from player to camera
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(cameraController.player.position, transform.position);
        }
    }
}
