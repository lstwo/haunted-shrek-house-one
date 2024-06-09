using Cinemachine;
using UnityEngine;

public class CinemachinePOVExtension : CinemachineExtension
{
    private float horizontalSpeed = SettingsManager.sensitivity;

    private float verticalSpeed = SettingsManager.sensitivity;

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
                startingRotation.x += deltaInput.x * horizontalSpeed / 165 * Time.timeScale;
                startingRotation.y += -deltaInput.y * verticalSpeed / 165 * Time.timeScale;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(startingRotation.y, startingRotation.x, 0f);
            }
        }
    }

    protected override void Awake()
    {
        horizontalSpeed = SettingsManager.sensitivity;
        verticalSpeed = SettingsManager.sensitivity;

        inputManager = InputManager.Instance;
        inputManager.SetCursorLock(true);
        base.Awake();
    }
}
