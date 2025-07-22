using UnityEngine;

[CreateAssetMenu(fileName = "chuteObj", menuName = "Prototype 3/ChuteObjs")]
public class ChuteObjData : ScriptableObject
{
    public GameObject[] model;
    public FlavourID flavourID;
    
}
public enum FlavourID
{
    Trash, Ingredient, Chocolate, Strawberry, IceCream
}
