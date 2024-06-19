using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    public ParticleSystem breakParticles;
    public GameObject gameObjectInCrate;
    public GameObject crateObject;

    [Space(10)]
    public bool destroyHammer = true;
    public string hammerTag = "Hammer";
    public float minVelocity = 2.5f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Hammer" && collision.rigidbody != null && (collision.rigidbody.velocity.magnitude > minVelocity || 
            (collision.rigidbody.gameObject.GetComponent<Item>() != null && collision.rigidbody.gameObject.GetComponent<Item>().fakeVelocity.magnitude > minVelocity)))
        {
            if (crateObject != null) 
                crateObject.SetActive(false);

            if (breakParticles != null) 
                breakParticles.Play();

            if (gameObjectInCrate != null) 
                gameObjectInCrate.SetActive(true);

            if (destroyHammer) 
                collision.rigidbody.gameObject.SetActive(false);
        }
    }
}
