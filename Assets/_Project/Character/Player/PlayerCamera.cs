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
    [Header("Minigame")]
    [SerializeField] private float focusTime;
    [SerializeField] private float focusFOV;
    [SerializeField] private Vector3 focusCameraPosition;
    [SerializeField] private Vector3 focusPointOffset;
    [Header("Sky")]
    [SerializeField] private Vector3 skyCameraPosition;
    [SerializeField] private Vector3 skyCameraAngle;
    [SerializeField] private float skyCameraMoveSpeed;
    [SerializeField] private float skyCameraRotationSpeed;

    private float normalFOV;
    private bool isOnMinigame = false;
    public bool isLookingAtSky = false;
    private bool isShakingCamera = false;
    private Vector3 cameraVelocity;

    public static PlayerCamera instance;

    private Vector3 startPosition;
    private Quaternion startRotation;

    private void Awake()
    {
        startPosition = cameraTransform.localPosition;
        startRotation = cameraTransform.localRotation;
        instance = this;
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
        if (isShakingCamera)
            return;

        if(isOnMinigame)
        {
            cameraTransform.LookAt(pivot.position);
        }
        else
        {
            cameraTransform.localRotation = startRotation;
        }
    }

    private void FixedUpdate()
    {
        if (isOnMinigame || isLookingAtSky)
        {
            return;
        }

        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, target.position, ref cameraVelocity, cameraSmoothSpeed);
        transform.position = targetCameraPosition;
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        if (isShakingCamera)
            return;

        StartCoroutine(ShakeCameraCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCameraCoroutine(float duration, float magnitude)
    {
        isShakingCamera = true;
        Vector3 startPosition = cameraTransform.localPosition;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1, 1) * magnitude;
            float y = Random.Range(-1, 1) * magnitude;

            cameraTransform.localPosition = new Vector3(x + startPosition.x, y + startPosition.y, startPosition.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = startPosition;
        isShakingCamera = false;
    }

}
