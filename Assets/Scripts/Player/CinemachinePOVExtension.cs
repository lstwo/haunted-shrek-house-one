using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    public float smoothing = 5f;

    private float horizontalSpeed = MainMenuManager.sensitivity;

    private float verticalSpeed = MainMenuManager.sensitivity;

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
                smoothedMouseDelta = Vector2.Lerp(smoothedMouseDelta, deltaInput, smoothing * Time.deltaTime);

                startingRotation.x += (smoothedMouseDelta.x * horizontalSpeed / 4 * Time.deltaTime) % 360;
                startingRotation.y += (-smoothedMouseDelta.y * verticalSpeed / 4 * Time.deltaTime) % 360;

                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(startingRotation.y % 360, startingRotation.x % 360, 0f);
            }
        }
    }

    protected override void Awake()
    {
        horizontalSpeed = MainMenuManager.sensitivity;
        verticalSpeed = MainMenuManager.sensitivity;

        inputManager = InputManager.Instance;
        inputManager.SetCursorLock(true);
        base.Awake();
    }
}
