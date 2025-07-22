using UnityEngine;

[CreateAssetMenu(fileName = "chuteObj", menuName = "Prototype 3/ChuteObjs")]
public class ChuteObjData : ScriptableObject
{
    public Mesh meshPrefab;
    public Material materialPrefab;
    public ChuteObject chuteObj;
    public int objectID;
    public int flavourID;
}
public enum ChuteObject
{
    Trash, Ingredient, Chocolate, Strawberry, IceCream
}
