using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Door : MonoBehaviour
{
    public GameObject normalDoor, brokenDoor;
    public bool destroyesItem = true;
    public DoorType type;
    public string keyTag = "Key";
    public string hammerTag = "Hammer";

    public enum DoorType
    {
        Broken,
        Key,
        Hammer,
        KeyOrHammer
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(type == DoorType.Broken)
        {
            if(collision.tag == "Player" && collision.gameObject.GetComponent<PlayerController>() != null && 
                collision.gameObject.GetComponent<PlayerController>().currentStage == PlayerController.PlayerStage.Sprinting ||  collision.tag == hammerTag)
            {
                normalDoor.SetActive(false);
                brokenDoor.SetActive(true);

                foreach (DoorField go in FloorManager.Instance.doorsToSave)
                {
                    if (go.door == gameObject)
                    {
                        go.hasBeenOpened = true;
                    }
                }
            }
        } else 
        {
            if ((type == DoorType.Key && collision.tag == keyTag) || (type == DoorType.Hammer && collision.tag == hammerTag) || 
                (type == DoorType.KeyOrHammer && (collision.tag == keyTag || collision.tag == hammerTag)))
            {
                foreach (DoorField go in FloorManager.Instance.doorsToSave)
                {
                    if (go.door == gameObject)
                    {
                        go.hasBeenOpened = true;
                    }
                }

                if (destroyesItem)
                {
                    GameManager.Instance.playerController.pickup.Drop();
                    Destroy(collision.gameObject);
                }

                normalDoor.SetActive(false);
                brokenDoor.SetActive(true);
            }
        }
    }
}
