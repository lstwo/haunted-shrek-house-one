using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Author: LeShaDDoW2
 */

public class Pickup : MonoBehaviour
{
        // Variables
    public GameObject holdingPoint;
    public float distance;
    public LayerMask layerMask;
    public LineRenderer lineRenderer;
    public float speed;
    public float maxDistanceToObject;
    public PlayerController player;
    public float gravityModifier = 1;

    Rigidbody pickupRb;
    GameObject pickupObject;

    bool lineEnabled;
    bool isHolding;

    void Start()
    {
        lineEnabled = false;
    }

    void Update()
    {
        MyInput();
        LineRendering();
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
            // Resetting all variables about the picked up object
        holdingPoint.transform.localPosition = Vector3.zero;

        pickupObject = null;
        pickupRb = null;

        lineEnabled = false;
        isHolding = false;

            // Resetting the line renderer
        lineRenderer.positionCount = 0;
    }

    void PickUp(RaycastHit hit)
    {
            // Setting the variables for the picked up Object
        pickupObject = hit.rigidbody.gameObject;
        pickupRb = hit.rigidbody;

            // Shooting the holding point forward to be on the level of the object
        holdingPoint.transform.position = hit.point;

            // Enable Holding and the line renderer
        lineEnabled = true;
        isHolding = true;
    }

    void LineRendering()
    {
             // Setting the Line Renderers positions to connect the picked up objects
             // and the area which it is moving to.
        if(lineEnabled && lineRenderer != null)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, holdingPoint.transform.position);
            lineRenderer.SetPosition(1, pickupObject.transform.position);
        }
    }

    void ObjectMoving()
    {
            // Seting the velocity of the picked up Object to the distance between the holding point and the object
        if(pickupRb != null)
        {
            Vector3 distance = (holdingPoint.transform.position - pickupRb.transform.position) * speed 
                / pickupRb.mass;
            pickupRb.velocity = distance;
        }
    }
}