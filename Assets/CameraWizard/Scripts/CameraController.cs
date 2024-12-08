using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public enum CameraMode
    {
        FollowPlayer,
        TopDown,
        SideScrolling,
        Transition
    }

    public CameraMode currentMode;

    public Transform player;
    public Transform lookAt;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    public Vector3 topDownOffset = new Vector3(0, 10, 0);
    public float sideScrollXOffset = 0;
    public float mouseSensitivity = 2f;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private void LateUpdate()
    {
        if (currentMode == CameraMode.FollowPlayer)
        {
            FollowPlayer();
        }
        else if (currentMode == CameraMode.TopDown)
        {
            TopDownView();
        }
        else if (currentMode == CameraMode.SideScrolling)
        {
            SideScrollingView();
        }
        else if(currentMode == CameraMode.Transition)
        {
            
        }
    }

    private void FollowPlayer()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Calculate rotation
        rotationX -= mouseY;  // Inverted for natural camera movement
        rotationY += mouseX;

        // Clamp vertical rotation only
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        // Calculate the rotated offset
        Vector3 rotatedOffset = Quaternion.Euler(rotationX, rotationY, 0) * offset;

        // Calculate camera position and rotation
        Vector3 desiredPosition = player.position + rotatedOffset;
        Quaternion desiredRotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Smooth both position and rotation
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);

        // Apply smoothed values
        transform.position = smoothedPosition;
        transform.rotation = smoothedRotation;
    }

    private void TopDownView()
    {
        transform.position = player.position + topDownOffset;
        transform.LookAt(lookAt);
    }

    private void SideScrollingView()
    {
        Vector3 sideScrollPosition = new Vector3(player.position.x + sideScrollXOffset, player.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, sideScrollPosition, smoothSpeed);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}