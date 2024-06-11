using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class GameProgress
{
    public FloorProgress[] floorProgress;

    public GameProgress()
    {

    }
}

[Serializable]
public class FloorProgress
{
    public int floorNumber;
    public DoorProgress[] doorProgress;

    public FloorProgress(int id, Dictionary<int, bool> doorProgress)
    {
        this.floorNumber = id;

        this.doorProgress = new DoorProgress[doorProgress.Count];

        for(int i = 0; i < doorProgress.Count; i++)
        {
            this.doorProgress[i] = new DoorProgress(doorProgress.Keys.ToArray()[i], doorProgress[doorProgress.Keys.ToArray()[i]]);
        }
    }

    public FloorProgress(int id, DoorField[] doorProgress)
    {
        this.floorNumber = id;

        this.doorProgress = new DoorProgress[doorProgress.Length];

        for (int i = 0; i < doorProgress.Length; i++)
        {
            this.doorProgress[i] = new DoorProgress(doorProgress[i].id, doorProgress[i].hasBeenOpened);
        }
    }

    public static Dictionary<int, bool> DeserializeDoorProgress(FloorProgress fp)
    {
        Dictionary<int, bool> dict = new Dictionary<int, bool>();
        
        foreach(DoorProgress dp in fp.doorProgress)
        {
            dict.Add(dp.id, dp.opened);
        }

        return dict;
    }
}

[Serializable]
public class DoorProgress
{
    public int id;
    public bool opened;

    public DoorProgress(int id, bool opened)
    {
        this.id = id;
        this.opened = opened;
    }
}