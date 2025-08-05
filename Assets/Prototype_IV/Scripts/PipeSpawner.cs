using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PROTOTYPE_4
{
    //This is a temp class, as I plan to add other obsticals later
    public class PipeSpawner : GameBehaviour
    {
        //Temp
        private GameManager gameManager;
        
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private GameObject spawnPoint;

        private void Start()
        {
            //Temp
            GameObject gameManagerobj = GameObject.Find("GameManager");
            gameManager = gameManagerobj.GetComponent<GameManager>();

            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while (gameManager.CurrentGameState() == GameState.PLAYING)
            {
                float rndHeight = Random.Range(-1f, 2f);
                float rndTime = Random.Range(3.5f, 6);

                SpawnColumn(rndHeight);

                yield return new WaitForSeconds(rndTime);
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
