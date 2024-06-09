using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Assigns")]
    public RectTransform staminometer;

    [Header("Funny Numbers")]
    public float playerSpeed = 1.0f;
    public float sprintMultiplier = 2.0f;
    [Range(0.0f, 100.0f)]
    public float maxStamina = 5.0f;
    public bool doSprint = true;
    public bool doStamina = true;

    [HideInInspector]
    public float stamina = 5.0f;

    private float maxStaminometerSize;

    private InputManager inputManager;
    private CharacterController controller;
    private Transform camTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        camTransform = Camera.main.transform;

        maxStaminometerSize = staminometer.sizeDelta.x;
        stamina = maxStamina;
    }

    void Update()
    {
        Vector2 input = inputManager.GetPlayerMovement();
        Vector3 move = new(input.x, 0f, input.y);

        if(doSprint)
        {
            if (inputManager.GetSprint() && stamina > 0 && move != Vector3.zero)
            {
                move *= sprintMultiplier;

                if(doStamina)
                {
                    stamina -= Time.deltaTime;
                    stamina = Mathf.Clamp(stamina, 0f, maxStamina);
                }
            }

            else if (move == Vector3.zero && doStamina)
            {
                stamina += Time.deltaTime * 1.5f;
                stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            }

            else if(doStamina)
            {
                stamina += Time.deltaTime * 0.3f;
                stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            }
        }

        if(doStamina)
            staminometer.sizeDelta = new(maxStaminometerSize / maxStamina * stamina, staminometer.sizeDelta.y);

        move = camTransform.forward * move.z + camTransform.right * move.x;
        move = new(move.x, 0, move.z);
        controller.Move(move * Time.deltaTime * playerSpeed);
    }
}
