using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    [SerializeField] private Transform pivot;
    [SerializeField] private float cameraSmoothSpeed;
    [Space]
    [SerializeField] private float focusTime;
    [SerializeField] private float focusFOV;
    [SerializeField] private Vector3 focusCameraPosition;
    [SerializeField] private Vector3 focusPointOffset;

    private float normalFOV;
    private bool isOnMinigame = false;
    private Vector3 cameraVelocity;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        startPosition = cameraTransform.position;
        startRotation = cameraTransform.rotation;

        cam = cameraTransform.GetComponent<Camera>();
        normalFOV = cam.fieldOfView;
    }

    private void Start()
    {
        MiniGameController.instance.OnStartMinimage.AddListener(FocusOnMinigame);
        MiniGameController.instance.OnCorrectMinigame.AddListener(UnfocusMinigame);
        MiniGameController.instance.OnWrongMinigame.AddListener(UnfocusMinigame);
    }

    private void UnfocusMinigame()
    {
        isOnMinigame = false;
    }

    private void FocusOnMinigame(Character otherTarget)
    {
        Vector3 middlePoint = (target.position + otherTarget.transform.position) / 2;
        pivot.position = middlePoint + focusPointOffset;
        cameraTransform.LookAt(pivot.position);
        isOnMinigame = true;
    }

    private void Update()
    {
        if (isOnMinigame)
        {
            if (cam.fieldOfView > focusFOV)
            {
                float fov = Mathf.Lerp(cam.fieldOfView, focusFOV, focusTime * Time.deltaTime);
                cam.fieldOfView = fov;
                if (cam.fieldOfView - fov < 0.1f)
                    cam.fieldOfView = fov;
            }

            if(Vector3.Distance(cameraTransform.localPosition, focusCameraPosition) > 0.1f)
            {
                Vector3 smoothedPostion = Vector3.Lerp(cameraTransform.localPosition, focusCameraPosition, focusTime * Time.deltaTime);
                cameraTransform.localPosition = smoothedPostion;

            }
            else
            {
                cameraTransform.localPosition = focusCameraPosition;
            }
            return;
        }

        if (cam.fieldOfView < normalFOV)
        {
            float fov = Mathf.Lerp(cam.fieldOfView, normalFOV, focusTime * Time.deltaTime);
            cam.fieldOfView = fov;
            if (cam.fieldOfView - fov < 0.1f)
                cam.fieldOfView = fov;
        }

        if (Vector3.Distance(cameraTransform.localPosition, startPosition) > 0.1f)
        {
            Vector3 smoothedPostion = Vector3.Lerp(cameraTransform.localPosition, startPosition, focusTime * Time.deltaTime);
            cameraTransform.localPosition = smoothedPostion;

        }
        else
        {
            cameraTransform.localPosition = startPosition;
        }

        if(Quaternion.Dot(cameraTransform.localRotation, startRotation) > 0.1f)
        {
            Quaternion smoothRotation = Quaternion.Lerp(cameraTransform.localRotation, startRotation, focusTime * Time.deltaTime);
            cameraTransform.localRotation = smoothRotation;
        }
        else
        {
            cameraTransform.localRotation = startRotation;
        }
    }

    private void FixedUpdate()
    {
        if(isOnMinigame)
        {
            return;
        }
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, target.position, ref cameraVelocity, cameraSmoothSpeed);
        transform.position = targetCameraPosition;
    }

}
