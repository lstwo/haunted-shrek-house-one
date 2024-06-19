using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
     float smoothing = 150f;

    private float horizontalSpeed = GameManager.sensitivity;

    private float verticalSpeed = GameManager.sensitivity;

    private Vector2 smoothedMouseDelta;

    [SerializeField]
    private float clampAngle = 80f;

    private InputManager inputManager;
    private Vector3 startingRotation;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (inputManager == null) return;
        if(vcam.Follow)
        {
            if(stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;

                Vector2 deltaInput = inputManager.GetMouseDelta();
                smoothedMouseDelta = Vector2.Lerp(smoothedMouseDelta, deltaInput, smoothing);

                startingRotation.x += smoothedMouseDelta.x * horizontalSpeed * Time.deltaTime % 360f;
                startingRotation.y += - smoothedMouseDelta.y * verticalSpeed * Time.deltaTime % 360f;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(startingRotation.y % 360f, startingRotation.x % 360f, 0f);
            }
        }
    }

    protected override void Awake()
    {
        horizontalSpeed = GameManager.sensitivity;
        verticalSpeed = GameManager.sensitivity;

        inputManager = InputManager.Instance;
        inputManager.SetCursorLock(true);
        base.Awake();
    }
}
