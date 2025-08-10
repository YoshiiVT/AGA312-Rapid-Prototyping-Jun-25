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

        [SerializeField] private List<FlavourID> flavoursInChute;
        [SerializeField] private List<ChuteObjData> chuteObjList;
        [SerializeField] private List<GameObject> objectsPlayed;

        [SerializeField] private GameObject chuteObjPrefab;

        public void StartGame()
        {
            ListX.ShuffleList(flavoursInChute);
            StartCoroutine(EmptyChute());
        }

        private IEnumerator EmptyChute()
        {
            while (flavoursInChute.Count > 0)
            {
                yield return new WaitForSeconds(1);
                SpawnObj();
            }
        }

        private void SpawnObj()
        {
            ChuteObjData _COD = chuteObjList.Find(x => x.flavourID == flavoursInChute[0]);
            print(_COD.flavourID);
            int modelID = Random.Range(0, _COD.model.Length);
            GameObject chuteObj = Instantiate(_COD.model[modelID], chuteDispenser.transform);

            objectsPlayed.Add(chuteObj);
            flavoursInChute.Remove(flavoursInChute[0]);
            
            if (chuteObjList.Count == 0) { _GM.EndGame();}
        }
    }
}
