using UnityEngine;

public class pf_SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private Vector2 spawnRange;

    private int enemyCount;
    private int wave;

    private void Awake()
    {
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsByType<pf_EnemyControl>(FindObjectsSortMode.None).Length;

        if (enemyCount == 0)
        {
            wave++;
            SpawnEnemy();
            EntitySpawner(powerUpPrefab);
        }
    }

    public void StartWave()
    {
        enabled = true;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < wave; i++)
        {
            EntitySpawner(enemyPrefab);
        }
    }

    private void EntitySpawner(GameObject entity)
    {
        Vector3 spawnPoint = new Vector3(
                Random.Range(spawnRange[0], spawnRange[1]),
                enemyPrefab.transform.position.y,
                Random.Range(spawnRange[0], spawnRange[1]));

        Instantiate(entity, spawnPoint, entity.transform.rotation);
    }
}
