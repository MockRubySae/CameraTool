using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraStateTransision : MonoBehaviour
{
    public CameraController cameraController;
    public float transitionSpeed = 2f;

    private void Start()
    {
        cameraController = GetComponent<CameraController>();
    }

    public void ChangeCameraMode(CameraController.CameraMode newMode)
    {
        StartCoroutine(SmoothTransition(newMode));
    }

    private IEnumerator SmoothTransition(CameraController.CameraMode newMode)
    {
        cameraController.currentMode = CameraController.CameraMode.Transition;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition;
        float elapsed = 0;

        // Determine target position based on new mode
        switch (newMode)
        {
            case CameraController.CameraMode.TopDown:
                targetPosition = cameraController.player.position + cameraController.topDownOffset;
                break;
            case CameraController.CameraMode.SideScrolling:
                targetPosition = new Vector3(cameraController.player.position.x + cameraController.sideScrollXOffset,
                    cameraController.player.position.y,
                    transform.position.z);
                break;
            default:
                targetPosition = cameraController.player.position + cameraController.offset;
                break;
        }

        while (elapsed < transitionSpeed)
        {
            float t = elapsed / transitionSpeed;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.LookAt(cameraController.lookAt);


            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraController.currentMode = newMode;
    }
}