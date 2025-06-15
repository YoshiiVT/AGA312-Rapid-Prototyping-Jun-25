using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : GameBehaviour
{
    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    [Header("WaveState")]
    private int currentWave;
    private float spawnRange = 9;
    [SerializeField, ReadOnly] private int enemyCount;

    [Header("References")]
    [SerializeField, ReadOnly] private BattleSystem _BS;

    void Start()
    {
        GameObject gmObject = GameObject.Find("GameManager");
        _BS = gmObject.GetComponent<BattleSystem>(); 
        currentWave = _BS.FindCurrentWave();

        SpawnEnemyWave(currentWave);
    }

    private void Update()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length; //FindObjectsSortMode???
        if (enemyCount == 0) { currentWave = _BS.NewWave(); SpawnEnemyWave(currentWave);}
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            GameObject enemyToSpawn = Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
            _BS.unitList.Add(enemyToSpawn);

        }
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
