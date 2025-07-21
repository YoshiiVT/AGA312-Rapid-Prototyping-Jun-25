using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PROTOTYPE_3
{
    public class CakeManager : GameBehaviour<CakeManager>
    {
        #region Singleton
        // Singleton Set Up

        public static CakeManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("Cake Manager already instanced");
                return;
            }
            Instance = this;
        }
        #endregion

        [SerializeField] private List<GameObject> trashList;
        [SerializeField] private List<GameObject> bowlList;

        public void addTrash(GameObject _trash)
        {
            trashList.Add(_trash);
        }

        public void addBowl(GameObject _bowl)
        {
            bowlList.Add(_bowl);
        }
    }
}
