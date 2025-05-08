using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject UI;
    public UnityEvent OnGameStart;

    private pf_SpawnManager spawnManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = FindFirstObjectByType<pf_SpawnManager>();
        var elavators = FindObjectsByType<Elevator>(FindObjectsSortMode.InstanceID);

        for (int i = 0; i < elavators.Length; i++)
        {
            OnGameStart.AddListener(elavators[i].OnGameStart);
        }
    }

    public void StartGame()
    {
        OnGameStart.Invoke();
        spawnManager.StartWave();
        UI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
