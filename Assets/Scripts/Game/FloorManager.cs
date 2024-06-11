using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [Header("Assigns")]
    public Transform playerSpawn;
    public DoorField[] doorsToSave;

    [Header("Funny Numbers")]
    [Tooltip("Check this if the player spawns in the elevator!")]
    public bool playerSpawnInElevator = false;

    [HideInInspector]
    public bool hasBeenCompleted = false;


    private static FloorManager _instance;

    public static FloorManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(_instance);
        else _instance = this;

        if(playerSpawnInElevator) GameManager.Instance.justLoadedFloor = true;

        if (hasBeenCompleted)
        {
            foreach (DoorField f in doorsToSave)
            {
                f.door.SetActive(!f.hasBeenOpened);
            }
        }
    }
}

[Serializable]
public class DoorField
{
    public int id;
    public GameObject door;
    public bool hasBeenOpened;
}
