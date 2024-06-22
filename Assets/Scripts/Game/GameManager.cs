using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Assigns")]
    public SceneField[] floors;
    public PlayerController playerController;
    public GameObject renderTextureScreen;
    public Animation savingTextAnimation;
    public AnimationClip saveTextPopup, saveTextGoAway;

    [Header("Funny Numbers")]
    public int startingFloor = -1;
    public static float sensitivity = 10;

    [HideInInspector] // Used for spawning in an elevator to not instantly load the next floor
    public bool justLoadedFloor = false;

    [HideInInspector]
    public int currentFloor = 1;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public static bool hasInstance;

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

        hasInstance = true;

        if (GameSaves.saves[GameSaves.currentSave].progress.currentFloor >= 0 && GameSaves.saves[GameSaves.currentSave].progress.currentFloor <= floors.Length)
        {
            SceneManager.LoadSceneAsync(floors[GameSaves.saves[GameSaves.currentSave].progress.currentFloor - 1], LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        renderTextureScreen.SetActive(CheckIfAnyFloorLoaded());
    }

    public bool CheckIfAnyFloorLoaded()
    {
        bool hasFloorLoaded = false;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            for (int j = 0; j < floors.Length; j++)
            {
                if (SceneManager.GetSceneAt(i).name == floors[j])
                {
                    hasFloorLoaded = true;
                    break;
                }
            }

            if (hasFloorLoaded) break;
        }

        return hasFloorLoaded;
    }

    public bool CheckIfFloorLoaded(int floor)
    {
        return SceneManager.GetSceneByName(floors[floor - 1]) != null;
    }

    private async void SpawnPlayer()
    {
        while (FloorManager.Instance == null) await Task.Delay(1);
        playerController.transform.position = FloorManager.Instance.playerSpawn.position;
    }

    public IEnumerator LoadFloor(int floorIndex)
    {
        if(floorIndex < floors.Length)
        {
            GameSaveManager.TryOverrideFloor(floorIndex + 1, FloorManager.Instance.ToFloorProgress());

            SaveSystem.SaveGame();

            justLoadedFloor = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync(floors[floorIndex], LoadSceneMode.Additive);

            currentFloor = floorIndex + 1;

            foreach (SceneField s in floors)
            {
                if (SceneManager.GetSceneByName(s.SceneName).isLoaded && floors.ToList().IndexOf(s) != floorIndex) SceneManager.UnloadSceneAsync(s);
            }
        }

        yield break;
    }
}
