using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject normalDoor, brokenDoor;
    public bool destroyesKey = true;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Key")
        {
            foreach(DoorField go in FloorManager.Instance.doorsToSave)
            {
                if(go.door == gameObject)
                {
                    go.hasBeenOpened = true;
                }
            }

            if(destroyesKey)
            {
                GameManager.Instance.playerController.pickup.Drop();
                Destroy(collision.gameObject);
            }
            
            normalDoor.SetActive(false);
            brokenDoor.SetActive(true);
        }
    }
}
