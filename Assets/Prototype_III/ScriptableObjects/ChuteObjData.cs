using UnityEngine;

[CreateAssetMenu(fileName = "chuteObj", menuName = "Prototype 3/ChuteObjs")]
public class ChuteObjData : ScriptableObject
{
    //public GameObject meshPrefab; Dont need this actually.
    public ChuteObject chuteObj;
    public int objectID;
    public int flavourID;

    //Temp: For this first prototype I will colour code the Chute OBJS and Cakes
    public Color meshColour;
}
public enum ChuteObject
{
    Trash, Ingredient, Chocolate, Strawberry, IceCream
}
