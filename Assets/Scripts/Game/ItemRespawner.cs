using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public Transform respawnTransform;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Rigidbody>())
        {
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = respawnTransform.position;
        }
    }
}
