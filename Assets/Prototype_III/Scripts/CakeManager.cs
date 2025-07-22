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
        public void SortBowl()
        {
            SortBowlLoop(() =>
            {
                print("Bowl Loop completed");
                BakeCake();
            });
        }
        
        public void SortBowlLoop(Action _onComplete = null)
        {
            foreach (GameObject _bowlOBJ in bowlList)
            {
                ChuteOBJ _bowlOBJType = _bowlOBJ.GetComponent<ChuteOBJ>();

                switch (_bowlOBJType.objectType)
                {
                    case FlavourID.Trash:
                        {
                            trashPurity.Add(_bowlOBJ);
                            break;
                        }
                    case FlavourID.Ingredient:
                        {
                            ingredientPurity.Add(_bowlOBJ);
                            break;
                        }
                    case FlavourID.Chocolate:
                        {
                            chocolatePurity.Add(_bowlOBJ);
                            break;
                        }
                    case FlavourID.Strawberry:
                        {
                            strawberryPurity.Add(_bowlOBJ);
                            break;
                        }
                    case FlavourID.IceCream:
                        {
                            icecreamPurity.Add(_bowlOBJ);
                            break;
                        }
                }
            }
            _onComplete?.Invoke();
        }

        public void BakeCake()
        {
            if (trashPurity.Count >= 4 || ingredientPurity.Count <= 2)
            {
                Debug.Log("You made a trash cake");
                _GM.CakeResult("Trash");
                return;
            }

            if (chocolatePurity.Count >= 4 && strawberryPurity.Count <= 2 && icecreamPurity.Count <= 2)
            {
                Debug.Log("You made a chocolate cake");
                _GM.CakeResult("Chocolate");
                return;
            }
            if (strawberryPurity.Count >= 4 && chocolatePurity.Count <= 2 && icecreamPurity.Count <= 2)
            {
                Debug.Log("You made a strawberry cake");
                _GM.CakeResult("Strawberry");
                return;
            }
            if (icecreamPurity.Count >= 4 && strawberryPurity.Count <= 2 && chocolatePurity.Count <= 2)
            {
                Debug.Log("You made an ice-cream cake");
                _GM.CakeResult("Ice-Cream");
                return;
            }
            if (icecreamPurity.Count <= 2 && strawberryPurity.Count <= 2 && chocolatePurity.Count <= 2)
            {
                Debug.Log("You made a vanilla cake");
                _GM.CakeResult("Vanila");
                return;
            }

            Debug.Log("You made a mixed cake");
            _GM.CakeResult("Mixed");
        }
    }
}
