using UnityEngine;

public class SpawnManager : GameBehaviour
{
    #region (References and Variables)
    [Header("Prefabs")]
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;

    [Header("WaveState")]
    private int currentWave;
    private float spawnRange = 9;
    [SerializeField, ReadOnly] private int enemyCount;

    [Header("References")]
    [SerializeField, ReadOnly] private BattleSystem _BS;
    #endregion

    #region (On GameStart Methods)
    /// <summary>
    /// This is temporary until I can figure out how to make a manager system using the new singleton + behaviour method.
    /// </summary>
    void Awake()
    {
        GameObject gmObject = GameObject.Find("GameManager");
        _BS = gmObject.GetComponent<BattleSystem>(); 
    }

    /// <summary>
    /// This is called by BattleSystem, and is used to start SpawnManager up
    /// </summary>
    /// <param name="enemiesToSpawn"></param>
    public void StartGame(int enemiesToSpawn)
    {
        SpawnEnemyWave(enemiesToSpawn);
    }
    #endregion

    /// <summary>
    /// This is called everytime an enemy dies. It Checks if there are any enemies left alive, if not it sets a new wave and spawns it in
    /// </summary>
    public void CheckEnemyCount()
    {
        enemyCount = FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length; //FindObjectsSortMode???
        if (enemyCount == 0) { currentWave = _BS.NewWave(); SpawnEnemyWave(currentWave);}
    }

    #region (Enemy and Powerup Spawning Methods)
    /// <summary>
    /// The two methods below generate enemies and powerup depending on how many is specified
    /// </summary>
    /// <param name="enemiesToSpawn"></param>
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            if (enemyPrefab == null)
            {
                Debug.LogError("Enemy Prefab is not assigned!");
                return;
            }

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
    #endregion
}
