using UnityEngine;

[CreateAssetMenu(fileName = "CakeData", menuName = "Prototype 3/CakeData")]
public class CakeData : ScriptableObject
{
    //public GameObject meshPrefab; Dont need this actually.
    public CakeFlavours cakeFlavours;
    [Range(1, 5)] public int trashPurity;
    [Range(1, 5)] public int ingredientPurity;
    [Range(1, 5)] public int chocolatePurity;
    [Range(1, 5)] public int strawberryPurity;
    [Range(1, 5)] public int iceCreamPurity;
    public int cakeID;

    //Temp: For this first prototype I will colour code the Chute OBJS and Cakes
    public Color meshColour;
}

public enum CakeFlavours
{
    TrashCake,
    Mixed,
    Vanilla,
    Chocolate,
    Strawberry,
    Ice_Cream
}
