using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : GameBehaviour
{
    private int currentWave;
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public GameObject powerupPrefab;

    [SerializeField, ReadOnly] private GameManager _GM;
    void Start()
    {

        GameObject gmObject = GameObject.Find("GameManager");
        _GM = gmObject.GetComponent<GameManager>(); 
        currentWave = _GM.FindCurrentWave();


        SpawnEnemyWave(currentWave);
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    private void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length; //FindObjectsSortMode???
        if (enemyCount == 0) { currentWave = _GM.NewWave(); SpawnEnemyWave(currentWave); Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation); }
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
