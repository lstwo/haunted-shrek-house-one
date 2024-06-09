using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private InputActions inputActions;

    public InputAction clickAction;

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

        inputActions = new InputActions();
        clickAction = inputActions.FindAction("Click");
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return inputActions.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseDelta()
    {
        return inputActions.Player.Look.ReadValue<Vector2>();
    }

    public bool GetSprint()
    {
        return inputActions.Player.Sprint.IsPressed();
    }

    public bool SetCursorLock(bool b)
    {
        Cursor.lockState = b ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !b;
        return b;
    }
}
