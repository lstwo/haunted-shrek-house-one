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

    public Transform debugTransform;

    [Header("Funny Numbers")]
    public float distance;
    public float throwSpeed;
    public float throwSmoothing;
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
            pickupObject.transform.parent = null;
            pickupRb.isKinematic = false;
            pickupRb.velocity = (pickupObject.GetComponent<Item>().fakeVelocity);

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

        pickupRb.isKinematic = true;

        pickupObject.transform.parent = holdingPoint.transform;
        pickupRb.transform.localPosition = Vector3.zero;
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
            if (Physics.Raycast(transform.position, transform.forward, out hit, (desiredPosition - transform.position).magnitude, envLayerMask))
            {
                // If there's a hit, set the desired position to the hit point
                desiredPosition = hit.point;
            }

            // Calculate the distance vector to move the object
            Vector3 distance = desiredPosition - pickupRb.transform.position;

            holdingPoint.transform.position = desiredPosition;

            pickupRb.transform.forward = transform.forward;
            pickupObject.GetComponent<Item>().fakeVelocity = Vector3.Lerp(pickupObject.GetComponent<Item>().fakeVelocity, distance.normalized * throwSpeed, throwSmoothing);
        }
    }
}

public static class ExtensionMethods
{
    /// <summary>
    /// Rounds Vector3.
    /// </summary>
    /// <param name="vector3"></param>
    /// <param name="decimalPlaces"></param>
    /// <returns></returns>
    public static Vector3 Round(this Vector3 vector3, int decimalPlaces = 2)
    {
        float multiplier = 1;
        for (int i = 0; i < decimalPlaces; i++)
        {
            multiplier *= 10f;
        }
        return new Vector3(
            Mathf.Round(vector3.x * multiplier) / multiplier,
            Mathf.Round(vector3.y * multiplier) / multiplier,
            Mathf.Round(vector3.z * multiplier) / multiplier);
    }
}