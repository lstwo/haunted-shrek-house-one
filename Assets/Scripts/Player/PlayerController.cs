using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Assigns")]
    public RectTransform staminometer;
    public Pickup pickup;

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
    private Rigidbody rb;
    private Transform camTransform;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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

        Vector3 horizontalForward = camTransform.forward;
        horizontalForward.y = 0;
        horizontalForward.Normalize();

        Vector3 horizontalRight = camTransform.right;
        horizontalRight.y = 0;
        horizontalRight.Normalize();

        move = horizontalForward * move.z + horizontalRight * move.x;

        rb.velocity = move * playerSpeed;
    }
}
