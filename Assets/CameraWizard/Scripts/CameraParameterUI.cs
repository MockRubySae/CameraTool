using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[ExecuteInEditMode]
public class CameraParameterUI : MonoBehaviour
{
    public CameraController cameraController;
    public CameraStateTransision cameraStateTransision;
    public Camera mainCam;
    private void Start()
    {
        cameraController = GetComponent<CameraController>();
        cameraStateTransision = GetComponent<CameraStateTransision>();
        mainCam = GetComponent<Camera>();
    }
    public Vector3 offsetToSet;
    public Vector3 topDownOffsetToSet;
    public float sideScrollerOffsetToSet;
    public void SetOffset()
    {
        cameraController.offset = offsetToSet;
    }

    public void SetSideScrollerOffset()
    {
        cameraController.sideScrollXOffset = sideScrollerOffsetToSet;
    }

    public void SetTopDownOffset()
    {
        cameraController.topDownOffset = topDownOffsetToSet;
    }
    
    public float speedToSet;
    public void SetSmoothSpeed()
    {
        cameraController.smoothSpeed = speedToSet;
    }

    public float boundary;
    public void SetBoundary()
    {
        CameraBoundaries boundaries = GetComponent<CameraBoundaries>();
        boundaries.radius = boundary;
    }

    public void SetCamTypeOrthographic()
    {
        mainCam.orthographic = true;
    }

    public void SetCamTypePerspective()
    {
        mainCam.orthographic = false;
    }
}
[CustomEditor(typeof(CameraParameterUI))]
class CameraParameterUIEditor: Editor
{
    public override void OnInspectorGUI()
    {
        CameraParameterUI cameraParameterUI = (CameraParameterUI)target;
        if(cameraParameterUI == null)return;
        
        //offset section
        GUILayout.Label("Offset Settings");
        cameraParameterUI.offsetToSet = EditorGUILayout.Vector3Field("Offset", cameraParameterUI.offsetToSet);
        if (GUILayout.Button("SetOffset"))
        {
            cameraParameterUI.SetOffset();
        }

        cameraParameterUI.sideScrollerOffsetToSet = EditorGUILayout.FloatField("SideScroller X Offset", cameraParameterUI.sideScrollerOffsetToSet);
        if (GUILayout.Button("Set Side Scroller x Offset"))
        {
            cameraParameterUI.SetSideScrollerOffset();
        }
        cameraParameterUI.topDownOffsetToSet = EditorGUILayout.Vector3Field("TopDown Offset", cameraParameterUI.topDownOffsetToSet);
        if (GUILayout.Button("Set TopDown Offset"))
        {
            cameraParameterUI.SetTopDownOffset();
        }
        // Speed section
        GUILayout.Label("Speed Settings"); 
        cameraParameterUI.speedToSet = EditorGUILayout.FloatField("Speed", cameraParameterUI.speedToSet);
        if (GUILayout.Button("Set Speed"))
        {
            cameraParameterUI.SetSmoothSpeed();
        }

        // Boundary section
        GUILayout.Label("Boundary Settings");
        cameraParameterUI.boundary = EditorGUILayout.FloatField("Boundary Radius", cameraParameterUI.boundary);
        if (GUILayout.Button("Set Boundaries"))
        {
            cameraParameterUI.SetBoundary();
        }
        
        if (cameraParameterUI.cameraStateTransision == null) return;

        GUILayout.Label("Transision Settings");
        cameraParameterUI.cameraStateTransision.transitionSpeed =
            EditorGUILayout.FloatField("Transision Speed", cameraParameterUI.cameraStateTransision.transitionSpeed);
        // Camera Mode buttons
        GUILayout.Label("Camera Modes");

        if (GUILayout.Button("FollowPlayer"))
        {
            cameraParameterUI.cameraStateTransision.ChangeCameraMode(CameraController.CameraMode.FollowPlayer);
        }

        if (GUILayout.Button("TopDown"))
        {
            cameraParameterUI.cameraStateTransision.ChangeCameraMode(CameraController.CameraMode.TopDown);
        }

        if (GUILayout.Button("SideScrolling"))
        {
            cameraParameterUI.cameraStateTransision.ChangeCameraMode(CameraController.CameraMode.SideScrolling);
        }

        GUILayout.Label("Camera Projection");
        if (GUILayout.Button("SetOrthographic"))
        {
            cameraParameterUI.SetCamTypeOrthographic();
        }
        if (GUILayout.Button("SetPerspective"))
        {
            cameraParameterUI.SetCamTypePerspective();
        }
    }
}