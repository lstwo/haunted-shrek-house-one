using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Door : MonoBehaviour
{
    [Header("Assigns")]
    public GameObject normalDoor, brokenDoor;
    public DoorType type;

    [Header("Funny Numbers")]
    public bool destroyesItem = true;

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
        if (normalDoor == null) return;

        // Broken Door
        if(type == DoorType.Broken)
        {
            if(collision.tag == "Player" && collision.gameObject.GetComponent<PlayerController>() != null && 
                collision.gameObject.GetComponent<PlayerController>().currentStage == PlayerController.PlayerStage.Sprinting ||  collision.tag == hammerTag)
            {
                SaveDoor();
                DestroyDoor();
            }
        } 

        // Item Door
        else 
        {
            if ((type == DoorType.Key && collision.tag == keyTag) || (type == DoorType.Hammer && collision.tag == hammerTag) || 
                (type == DoorType.KeyOrHammer && (collision.tag == keyTag || collision.tag == hammerTag)))
            {
                if (destroyesItem)
                {
                    DestroyKey(collision.gameObject);
                }

                SaveDoor();
                DestroyDoor();
            }
        }
    }

    void DestroyKey(GameObject g)
    {
        GameManager.Instance.playerController.pickup.Drop();
        Destroy(g);
    }

    void DestroyDoor()
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        Destroy(normalDoor);
        brokenDoor.SetActive(true);
    }

    void SaveDoor()
    {
        foreach (DoorField go in FloorManager.Instance.doorsToSave)
        {
            if (go.door == gameObject)
            {
                go.hasBeenOpened = true;
            }
        }
    }
}
