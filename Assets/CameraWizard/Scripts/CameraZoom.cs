using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float minZoom = 5f;
    public float maxZoom = 15f;

    private CameraController cameraController;

    private void Start()
    {
        cameraController = GetComponent<CameraController>();
    }

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // Get current offset magnitude
            float currentZoom = cameraController.offset.magnitude;

            // Calculate new zoom level
            float newZoom = currentZoom - scroll * zoomSpeed;
            newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

            // Maintain direction while changing magnitude
            cameraController.offset = cameraController.offset.normalized * newZoom;
        }
    }
}
