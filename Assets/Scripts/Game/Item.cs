using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.activeInHierarchy) 
        { 
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            for(int i = 0; i < GetComponentsInChildren<Collider>().Length; i++)
            {
                Physics.IgnoreCollision(GetComponentsInChildren<Collider>()[i], collision.collider);
            }
        }
    }
}
