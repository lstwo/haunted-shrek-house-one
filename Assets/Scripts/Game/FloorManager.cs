using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager : MonoBehaviour
{
    [Header("Assigns")]
    public Transform playerSpawn;
    public DoorField[] doorsToSave;

    [Header("Funny Numbers")]
    [Tooltip("Check this if the player spawns in the elevator!")]
    public bool playerSpawnInElevator = false;
    public int floorNumber = 1;

    [HideInInspector]
    public bool hasBeenCompleted = false;


    private static FloorManager _instance;

    public static FloorManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this) Destroy(_instance.gameObject);
        _instance = this;

        if (GameManager.Instance == null) SceneManager.LoadScene("TheGame", LoadSceneMode.Additive);

        if(playerSpawnInElevator) GameManager.Instance.justLoadedFloor = true;

        if (GameSaveManager.GetFloorProgress(floorNumber) != null)
        {
            for(int i = 0; i < doorsToSave.Length; i++)
            {
                doorsToSave[i].hasBeenOpened = GameSaveManager.GetFloorProgress(floorNumber).doorProgress[i].opened;
                Debug.Log(i + "" + GameSaveManager.GetFloorProgress(floorNumber).doorProgress[i].opened);
            }

            foreach (DoorField f in doorsToSave)
            {
                f.door.SetActive(!f.hasBeenOpened);
            }
        }
    }

    public SerializableFloorProgress ToFloorProgress()
    {
        SerializableFloorProgress progress = new(floorNumber, doorsToSave);
        return progress;
    }
}

[Serializable]
public class DoorField
{
    public int id;
    public GameObject door;
    public bool hasBeenOpened;
}
