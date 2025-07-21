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

        [SerializeField, ReadOnly] private List<GameObject> trashPurity;
        [SerializeField, ReadOnly] private List<GameObject> ingredientPurity;
        [SerializeField, ReadOnly] private List<GameObject> chocolatePurity;
        [SerializeField, ReadOnly] private List<GameObject> strawberryPurity;
        [SerializeField, ReadOnly] private List<GameObject> icecreamPurity;

        public void addTrash(GameObject _trash)
        {
            trashList.Add(_trash);
        }

        public void addBowl(GameObject _bowl)
        {
            bowlList.Add(_bowl);
        }

        public void StortBowl()
        {
            foreach (GameObject _bowlOBJ in bowlList)
            {
                ChuteOBJ _bowlOBJType = _bowlOBJ.GetComponent<ChuteOBJ>();

                switch (_bowlOBJType.objectType)
                {
                    case ChuteObject.Trash:
                        {
                            trashPurity.Add(_bowlOBJ);
                            break;
                        }
                    case ChuteObject.Ingredient:
                        {
                            ingredientPurity.Add(_bowlOBJ);
                            break;
                        }
                    case ChuteObject.Chocolate:
                        {
                            chocolatePurity.Add(_bowlOBJ);
                            break;
                        }
                    case ChuteObject.Strawberry:
                        {
                            strawberryPurity.Add(_bowlOBJ);
                            break;
                        }
                    case ChuteObject.IceCream:
                        {
                            icecreamPurity.Add(_bowlOBJ);
                            break;
                        }
                }
            }
        }
    }
}
