using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author: LeShaDDoW2
 */

public class Pickup : MonoBehaviour
{
    // Variables
    [Header("Assigns")]
    public GameObject holdingPoint;
    public LayerMask layerMask;
    public LayerMask envLayerMask;
    public PlayerController player;

    [Header("Funny Numbers")]
    public float distance;
    public float speed;
    public float maxDistanceToObject;
    public float gravityModifier = 1;
    public float rotationSpeed = 100f;

    Rigidbody pickupRb;
    GameObject pickupObject;

    Vector3 maxHoldingPosition;

    bool isHolding;

    void Update()
    {
        MyInput();
    }

    private void FixedUpdate()
    {
        ObjectMoving();
    }

    void MyInput()
    {
            // Picking Up
        /*  
         *  When you click down the right mouse button:
         *    Cast a Ray from the Camera forwards and if it hits something that you can pickup,
         *    call the Pickup Function with the rigidbody of the hit Object.
         */
        RaycastHit hit;
        if(Input.GetMouseButtonDown(0) && 
            Physics.Raycast(transform.position, transform.forward, out hit, distance, layerMask) &&
            hit.rigidbody != null) 
        {
            PickUp(hit);
        }

            // Dropping
        /*
         *  When Letting go of the Right Mouse Button, call the Drop function.
         */
        if(Input.GetMouseButtonUp(0) && pickupObject != null) 
        {
            Drop();
        }
    }

    public void Drop()
    {
        if(isHolding)
        {
            // Resetting all variables about the picked up object
            holdingPoint.transform.localPosition = Vector3.zero;

            pickupObject = null;
            pickupRb = null;

            isHolding = false;
        }
    }

    void PickUp(RaycastHit hit)
    {
            // Setting the variables for the picked up Object
        pickupObject = hit.rigidbody.gameObject;
        pickupRb = hit.rigidbody;

            // Shooting the holding point forward to be on the level of the object
        holdingPoint.transform.position = hit.point;

            // Enable Holding and the line renderer
        isHolding = true;
    }

    void ObjectMoving()
    {
        // Seting the velocity of the picked up Object to the distance between the holding point and the object
        if (pickupRb != null)
        {
            // Calculate the desired position based on holdingPoint
            Vector3 desiredPosition = holdingPoint.transform.position;

            // Perform raycast to check if there's an obstacle between the current position and the holding point
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (desiredPosition - transform.position).normalized, out hit, (desiredPosition - transform.position).magnitude, envLayerMask))
            {
                // If there's a hit, set the desired position to the hit point
                desiredPosition = hit.point;
            }

            // Calculate the distance vector to move the object
            Vector3 distance = desiredPosition - pickupRb.transform.position;

            // Smoothly interpolate the position or apply a force
            pickupRb.velocity = distance * speed;

            pickupRb.transform.forward = transform.forward;
        }
    }
}