using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace PROTOTYPE_3
{
    public class ChuteManager : GameBehaviour<ChuteManager>
    {
        #region Singleton
        // Singleton Set Up

        public static ChuteManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Chute Manager already instanced");
                return;
            }
            Instance = this;
        }
        #endregion

        [SerializeField] private GameObject chuteDispenser;

        [SerializeField] private List<ChuteObjData> chuteObjList;
        [SerializeField] private List<GameObject> objectsPlayed;

        [SerializeField] private GameObject chuteObjPrefab;

        [SerializeField, ReadOnly] private int chuteObjListCount;

        private int objectCounter;

        public void StartGame()
        {
            chuteObjListCount = chuteObjList.Count;
            ListX.ShuffleList(chuteObjList);
            StartCoroutine(EmptyChute());
        }

        private IEnumerator EmptyChute()
        {
            yield return new WaitForSeconds(1);
            SpawnObj();
            if (objectCounter <= chuteObjListCount) StartCoroutine(EmptyChute());
        }

        private void SpawnObj()
        {
            if(objectCounter <= chuteObjListCount - 1) 
            {
                GameObject chuteObj = Instantiate(chuteObjPrefab, chuteDispenser.transform);
                chuteObj.GetComponent<ChuteOBJ>().Initialize(chuteObjList[0]);

                objectsPlayed.Add(chuteObj);
                chuteObjList.Remove(chuteObjList[0]);
                objectCounter++;
            }
            else { _GM.EndGame(); objectCounter++; }
        }
    }
}
