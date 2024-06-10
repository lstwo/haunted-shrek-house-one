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
        _animation.clip = saveFileSelectClip;
        _animation.Play();
    }
}

public static class GameSaves
{

} 

public class GameSave
{

}