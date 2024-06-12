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

    [Header("Funny Numbers")]
    public int startingFloor = -1;

    [HideInInspector] // Used for spawning in an elevator to not instantly load the next floor
    public bool justLoadedFloor = false;

    [HideInInspector]
    public int currentFloor = 1;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

        if (GameSaves.saves[GameSaves.currentSave].progress.currentFloor >= 0 && GameSaves.saves[GameSaves.currentSave].progress.currentFloor <= floors.Length)
        {
            SceneManager.LoadScene(floors[GameSaves.saves[GameSaves.currentSave].progress.currentFloor - 1], LoadSceneMode.Additive);
        }
    }

    private void Start()
    {
        SpawnPlayer();
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
            if(floorIndex > currentFloor - 1)
            {
                GameSaveManager.TryOverrideFloor(floorIndex + 1, FloorManager.Instance.ToFloorProgress());
            }

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
