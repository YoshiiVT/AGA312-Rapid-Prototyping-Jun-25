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
        [SerializeField] private GameObject obsticleParent;

        

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
                int rndHeight = Random.Range(-10, 10);
                int rndTime = Random.Range(2, 5);

                SpawnColumn(rndHeight);

                yield return new WaitForSeconds(rndTime);
            }
        }

        private void SpawnColumn(int rndHeight)
        {
            //Spawns columns
            GameObject pipeSpawn = Instantiate(pipePrefab, spawnPoint.transform.position, Quaternion.identity);
            pipeSpawn.transform.SetParent(obsticleParent.transform);

            // Adjust y-position
            Vector3 newPos = pipeSpawn.transform.position;
            newPos.y += rndHeight;
            pipeSpawn.transform.position = newPos;
        }
    }
}
