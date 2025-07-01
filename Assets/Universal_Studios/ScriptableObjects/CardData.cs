using UnityEngine;


[CreateAssetMenu(fileName ="Card", menuName ="Cards", order =1)]
public class CardData : ScriptableObject
{
    [Range(1, 52)] public int cardID;
    public Suite suite;
    [Range(1,13)] public int value;
    public Color colour;
    public string description;
    public Sprite icon;
}

public enum Suite { Diamond, Heart, Club, Spade}

