using UnityEngine;

namespace PROTOTYPE_2
{
    public class Column : MonoBehaviour
    {
        [SerializeField] private Column nextColumn;
        [SerializeField] private Column prevColumn;
        [SerializeField] private bool isEnd;
        [SerializeField] private bool isStart;

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

        public bool IsEnd()
        {
            if (!isEnd) return false;
            return true;
        }

        public bool IsStart()
        {
            if (!isStart) return false;
            return true;
        }
    }
}
