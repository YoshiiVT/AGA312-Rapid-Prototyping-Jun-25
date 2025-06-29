using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField] private Column nextColumn;
    [SerializeField] private Column prevColumn;

    public Column GetNextColumn()
    {
        if (nextColumn == null) Debug.LogError("The Next Column is Null");
        return nextColumn;
    }

    public Column GetPrevColumn()
    {
        if (prevColumn == null) Debug.LogError("The Previous Column is Null");
        return prevColumn;
    }
}
