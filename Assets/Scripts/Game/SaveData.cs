using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SerializableGameProgress
{
    public int currentFloor = 1;
    public SerializableFloorProgress[] floorProgress;

    public SerializableGameProgress(int currentFloor, SerializableFloorProgress[] floorProgress)
    {
        this.currentFloor = currentFloor;
        this.floorProgress = floorProgress;
    }

    public GameProgress Deserialize()
    {
        return new GameProgress(currentFloor, floorProgress.ToList());
    }
}

public class GameProgress
{
    public int currentFloor;
    public List<SerializableFloorProgress> floorProgress = new List<SerializableFloorProgress>();

    public GameProgress(int currentFloor, List<SerializableFloorProgress> floorProgress)
    {
        this.currentFloor=currentFloor;
        this.floorProgress=floorProgress;
    }

    public static GameProgress Empty()
    {
        return new GameProgress(1, new());
    }

    public SerializableGameProgress Serialize()
    {
        return new SerializableGameProgress(currentFloor, floorProgress.ToArray());
    }
}

[Serializable]
public class SerializableFloorProgress
{
    public int floorNumber;
    public SerializableDoorProgress[] doorProgress;

    public SerializableFloorProgress(int id, Dictionary<int, bool> doorProgress)
    {
        this.floorNumber = id;

        this.doorProgress = new SerializableDoorProgress[doorProgress.Count];

        for(int i = 0; i < doorProgress.Count; i++)
        {
            this.doorProgress[i] = new SerializableDoorProgress(doorProgress.Keys.ToArray()[i], doorProgress[doorProgress.Keys.ToArray()[i]]);
        }
    }

    public SerializableFloorProgress(int id, DoorField[] doorProgress)
    {
        this.floorNumber = id;

        this.doorProgress = new SerializableDoorProgress[doorProgress.Length];

        for (int i = 0; i < doorProgress.Length; i++)
        {
            this.doorProgress[i] = new SerializableDoorProgress(doorProgress[i].id, doorProgress[i].hasBeenOpened);
        }
    }

    public static Dictionary<int, bool> DeserializeDoorProgress(SerializableFloorProgress fp)
    {
        Dictionary<int, bool> dict = new Dictionary<int, bool>();
        
        foreach(SerializableDoorProgress dp in fp.doorProgress)
        {
            dict.Add(dp.id, dp.opened);
        }

        return dict;
    }
}

[Serializable]
public class SerializableDoorProgress
{
    public int id;
    public bool opened;

    public SerializableDoorProgress(int id, bool opened)
    {
        this.id = id;
        this.opened = opened;
    }
}