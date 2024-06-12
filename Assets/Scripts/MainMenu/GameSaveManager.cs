using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    private static GameSaveManager _instance;

    public Animation _animation;
    public AnimationClip saveFileSelectClip;

    public static GameSaveManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }

    public void SelectGameSave(int id)
    {
        GameSaves.saves = SaveSystem.LoadGame();
        GameSaves.currentSave = id;
        _animation.clip = saveFileSelectClip;
        _animation.Play();
    }

    public static void CompleteFloor(int newFloorNumber, SerializableFloorProgress floorProgress)
    {
        GameSaves.saves[GameSaves.currentSave].progress.currentFloor = newFloorNumber;
        GameSaves.saves[GameSaves.currentSave].progress.floorProgress.Add(floorProgress);
        SaveSystem.SaveGame();
    }

    public static bool OverrideFloor(SerializableFloorProgress floorProgress)
    {
        bool success = false;
        int floor = floorProgress.floorNumber;
        foreach(SerializableFloorProgress progress in GameSaves.saves[GameSaves.currentSave].progress.floorProgress)
        {
            if(progress.floorNumber == floor)
            {
                GameSaves.saves[GameSaves.currentSave].progress.floorProgress
                    [GameSaves.saves[GameSaves.currentSave].progress.floorProgress.IndexOf(progress)] = floorProgress;
                success = true;
                break;
            }
        }
        SaveSystem.SaveGame();
        return success;
    }

    public static void TryOverrideFloor(int newFloorNumber, SerializableFloorProgress floorProgress)
    {
        if(!OverrideFloor(floorProgress))
        {
            CompleteFloor(newFloorNumber, floorProgress);
        }
    }

    public static SerializableFloorProgress GetFloorProgress(int floorNumber)
    {
        foreach(SerializableFloorProgress save in GameSaves.saves[GameSaves.currentSave].progress.floorProgress)
        {
            if(save.floorNumber == floorNumber)
            {
                return save;
            }
        }
        return null;
    }
}

public static class GameSaves
{
    public static int currentSave = 0;
    private static GameSave SAVE_1 = GameSave.Empty(), SAVE_2 = GameSave.Empty(), SAVE_3 = GameSave.Empty();
    public static GameSave[] saves = new GameSave[3] { SAVE_1, SAVE_2, SAVE_3 };
} 

public class GameSave
{
    public string name;
    public GameProgress progress;

    public GameSave(string name, GameProgress progress)
    {
        this.name = name;
        this.progress = progress;
    }

    public static GameSave Empty()
    {
        return new GameSave("Name", GameProgress.Empty());
    }

    public SerializableGameSave Serialize()
    {
        return new SerializableGameSave(name, progress.Serialize());
    }
}

[Serializable] 
public class SerializableGameSave
{
    public string name;
    public SerializableGameProgress progress;

    public SerializableGameSave(string name, SerializableGameProgress progress)
    {
        this.name = name;
        this.progress = progress;
    }

    public GameSave Deserialize()
    {
        return new GameSave(name, progress.Deserialize());
    }
}