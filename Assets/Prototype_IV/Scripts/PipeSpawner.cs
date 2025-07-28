using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PROTOTYPE_4
{
    //This is a temp class, as I plan to add other obsticals later
    public class PipeSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        private bool endStartingColumns;

        private void Start()
        {
            //Start spawning first few columns
            StartCoroutine(StartingColumns()); StartingColumns();
        }

        private IEnumerator StartingColumns()
        {
            //Spawns first few columns
            yield return new WaitForSeconds(1.5f);
            SpawnColumn();
            if (endStartingColumns == false)
            {
                StartCoroutine(StartingColumns());
            }
        }

        private void SpawnColumn()
        {
            //Spawns columns
            Instantiate(pipePrefab);
        }
    }
}
