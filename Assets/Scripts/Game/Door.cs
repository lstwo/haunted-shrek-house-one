using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject normalDoor, brokenDoor;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Key")
        {
            GameManager.Instance.playerController.pickup.Drop();
            Destroy(collision.gameObject);
            normalDoor.SetActive(false);
            brokenDoor.SetActive(true);
        }
    }
}
