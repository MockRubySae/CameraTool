using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraWizzardSetUp : EditorWindow
{
    [MenuItem("Tools/CameraWizzard")]
    public static void ShowWindow()
    {
        GetWindow<CameraWizzardSetUp>("Camera Wizzard");
    }

    private void OnGUI()
    {
        GUILayout.Label("Camera Setup", EditorStyles.boldLabel);

        if (GUILayout.Button("Setup Camera"))
        {
            SetupCamera();
        }
    }

    private void SetupCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found in scene!");
            return;
        }

        // Add required components if they don't exist
        if (!mainCamera.GetComponent<CameraController>())
            mainCamera.gameObject.AddComponent<CameraController>();

        if (!mainCamera.GetComponent<CameraStateTransision>())
            mainCamera.gameObject.AddComponent<CameraStateTransision>();

        if (!mainCamera.GetComponent<CameraParameterUI>())
            mainCamera.gameObject.AddComponent<CameraParameterUI>();

        if (!mainCamera.GetComponent<CameraZoom>())
            mainCamera.gameObject.AddComponent<CameraZoom>();

        if (!mainCamera.GetComponent<CameraBoundaries>())
            mainCamera.gameObject.AddComponent<CameraBoundaries>();
        if (!mainCamera.GetComponent<CameraShake>())
            mainCamera.gameObject.AddComponent<CameraShake>();

        // Setup references
        CameraParameterUI parameterUI = mainCamera.GetComponent<CameraParameterUI>();
        parameterUI.cameraController = mainCamera.GetComponent<CameraController>();
        parameterUI.cameraStateTransision = mainCamera.GetComponent<CameraStateTransision>();
        parameterUI.mainCam = mainCamera;

        CameraStateTransision stateTransition = mainCamera.GetComponent<CameraStateTransision>();
        stateTransition.cameraController = mainCamera.GetComponent<CameraController>();

        Debug.Log("Camera setup complete!");
    }
}
