using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PROTOTYPE_4
{
    //This is a temp class, as I plan to add other obsticals later
    public class PipeSpawner : GameBehaviour
    {
        [Header("Managers")]
        //Temp
        [SerializeField] private GameManager gameManager;

        [Header("References")]
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private GameObject spawnPoint;

        [Header("Variables")]
        [SerializeField, ReadOnly] private float spawnTime;
        

        private void Start()
        {
            //Temp
            GameObject gameManagerobj = GameObject.Find("GameManager");
            gameManager = gameManagerobj.GetComponent<GameManager>();
            if (gameManager == null) { Debug.LogError("GAMEMANAGER NOT FOUND!!!"); }

            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (gameManager.CurrentGameState() == GameState.PLAYING)
            {
                float rndHeight = Random.Range(-1f, 2f);

                SpawnColumn(rndHeight);

                switch (gameManager.speed)
                {
                    case < 2:
                        spawnTime = Random.Range(3.5f, 5.5f);
                        break;
                    case < 3:
                        spawnTime = Random.Range(2f, 4.5f);
                        break;
                    case < 4:
                        spawnTime = Random.Range(1.75f, 3f);
                        break;
                    case < 5:
                        spawnTime = Random.Range(1.75f, 2.5f);
                        break;
                    case < 6:
                        spawnTime = Random.Range(1.55f, 2f);
                        break;
                    case < 7:
                        spawnTime = Random.Range(1.5f, 1.75f);
                        break;
                    case < 8:
                        spawnTime = Random.Range(1.25f, 1.5f);
                        break;
                    case < 9:
                        spawnTime = Random.Range(1f, 1.5f);
                        break;
                    case <= 10:
                        spawnTime = Random.Range(0.5f, 1f);
                        break;
                }
                yield return new WaitForSeconds(spawnTime);
            }
        }

        private void SpawnColumn(float rndHeight)
        {
            //Spawns columns
            GameObject pipeSpawn = Instantiate(pipePrefab, spawnPoint.transform.position, Quaternion.identity);

            // Adjust y-position
            Vector3 newPos = pipeSpawn.transform.position;
            newPos.y += rndHeight;
            pipeSpawn.transform.position = newPos;
        }
    }
}
