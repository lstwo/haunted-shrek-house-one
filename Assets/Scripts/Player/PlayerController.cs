using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Assigns")]
    public RectTransform staminometer;
    public Pickup pickup;
    public Animation headAnimation;

    public AnimationClip idleHeadAnimation, walkingHeadAnimation, sprintingHeadAnimation;

    [Header("Funny Numbers")]
    public float playerSpeed = 1.0f;
    public float sprintMultiplier = 2.0f;

    [Range(0.0f, 100.0f)]
    public float maxStamina = 5.0f;

    public float staminaUseMultiplier = 1f;
    public float staminaRegenStillMultiplier = 1.5f;
    public float staminaRegenWalkMultiplier = 0.2f;

    public bool doSprint = true;
    public bool doStamina = true;

    [HideInInspector]
    public float stamina = 5.0f;

    [HideInInspector]
    public PlayerStage currentStage;

    private float maxStaminometerSize;

    private InputManager inputManager;
    private Rigidbody rb;
    private Transform camTransform;

    public enum PlayerStage
    {
        Still,
        Walking,
        Sprinting
    }

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
                currentStage = PlayerStage.Sprinting;
                headAnimation.clip = (sprintingHeadAnimation);
                headAnimation.Play();

                move *= sprintMultiplier;

                if(doStamina)
                {
                    stamina -= Time.deltaTime * staminaUseMultiplier;
                    stamina = Mathf.Clamp(stamina, 0f, maxStamina);
                }
            }

            else if (move == Vector3.zero && doStamina)
            {
                currentStage = PlayerStage.Still;
                headAnimation.clip = (idleHeadAnimation);
                headAnimation.Play();

                stamina += Time.deltaTime * staminaRegenStillMultiplier;
                stamina = Mathf.Clamp(stamina, 0f, maxStamina);
            }

            else if(doStamina && stamina > 0)
            {
                currentStage = PlayerStage.Walking;
                headAnimation.clip = (walkingHeadAnimation);
                headAnimation.Play();

                stamina += Time.deltaTime * staminaRegenWalkMultiplier;
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
