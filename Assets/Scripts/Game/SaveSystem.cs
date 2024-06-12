using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/lstwoSTUDIOS/")) 
            Directory.CreateDirectory(Application.persistentDataPath + "/lstwoSTUDIOS/");

        if (!Directory.Exists(Application.persistentDataPath + "/lstwoSTUDIOS/HauntedShrekHouseOne/")) 
            Directory.CreateDirectory(Application.persistentDataPath + "/lstwoSTUDIOS/HauntedShrekHouseOne/");

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/lstwoSTUDIOS/HauntedShrekHouseOne/";

        for(int i = 0; i < GameSaves.saves.Length; i++)
        {
            FileStream stream = new FileStream(path + "save_" + i + ".shrek", FileMode.Create);

            formatter.Serialize(stream, GameSaves.saves[i].Serialize());
            stream.Close();
        }
    }

    public static GameSave[] LoadGame()
    {
        GameSave[] saves = new GameSave[GameSaves.saves.Length];

        BinaryFormatter formatter = new BinaryFormatter();

        for (int i = 0; i < GameSaves.saves.Length; i++)
        {
            string path = Application.persistentDataPath + "/lstwoSTUDIOS/HauntedShrekHouseOne/save_" + i + ".shrek";
            if(File.Exists(path))
            {
                FileStream stream = new FileStream(path, FileMode.Open);
                saves[i] = ((SerializableGameSave)formatter.Deserialize(stream)).Deserialize();
                try
                {
                    Debug.Log(saves[i].progress.floorProgress[0].doorProgress[0].opened + "sdgjfk");
                }
                catch (Exception e) { }
                stream.Close();
            } else
            {
                Debug.LogError("Save file not found in " + path);
            }
        }

        return saves;
    }
}