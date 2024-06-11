using System.Collections;
using System.Linq;
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

    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;

        if(startingFloor >= 0 && startingFloor < floors.Length)
            SceneManager.LoadSceneAsync(floors[startingFloor], LoadSceneMode.Additive);
    }

    public IEnumerator LoadFloor(int floorIndex)
    {
        if(floorIndex < floors.Length)
        {
            justLoadedFloor = true;
            AsyncOperation operation = SceneManager.LoadSceneAsync(floors[floorIndex], LoadSceneMode.Additive);

            foreach (SceneField s in floors)
            {
                if (SceneManager.GetSceneByName(s.SceneName).isLoaded && floors.ToList().IndexOf(s) != floorIndex) SceneManager.UnloadSceneAsync(s);
            }
        }

        yield break;
    }
}
