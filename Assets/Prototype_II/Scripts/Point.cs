using UnityEngine;

namespace PROTOTYPE_2
{
    public class Point : MonoBehaviour
    {
        [SerializeField] private Point nextPoint;
        [SerializeField] private Point prevPoint;
        [SerializeField] private bool isEnd;
        [SerializeField] private bool isStart;

        public Point GetNextKey()
        {
            if (nextPoint == null) Debug.LogError("The Next Point is Null");
            return nextPoint;
        }

        public Point GetPrevKey()
        {
            if (prevPoint == null) Debug.LogError("The Previous Point is Null");
            return prevPoint;
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
