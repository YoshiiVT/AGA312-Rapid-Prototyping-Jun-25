using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace PROTOTYPE_3
{
    public class ChuteManager : GameBehaviour<ChuteManager>
    {
        [SerializeField] private GameObject chuteDispenser;

        [SerializeField] private List<ChuteObjData> chuteObjList;

        [SerializeField] private GameObject chuteObjPrefab;

        private int objectCounter;
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W)) SpawnObj();
        }

        private void SpawnObj()
        {
            if(objectCounter <= chuteObjList.Count) 
            {
                GameObject chuteObj = Instantiate(chuteObjPrefab, chuteDispenser.transform);
                chuteObj.GetComponent<ChuteOBJ>().Initialize(chuteObjList[objectCounter]);
            }
            
        }
    }
}
