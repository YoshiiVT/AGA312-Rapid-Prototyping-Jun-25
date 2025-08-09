using UnityEngine;

namespace PROTOTYPE_2
{
    public class Point : GameBehaviour
    {
        [Header("Point References")]
        [SerializeField] private Point nextPoint;
        [SerializeField] private Point prevPoint;

        [Header("Point Variables")]
        [SerializeField] private bool isEnd;
        [SerializeField] private bool isCenter;
        [SerializeField] private bool justPassedCenter;
        [SerializeField] private bool isStart;

        public Point GetNextPoint()
        {
            if (nextPoint == null) Debug.LogError("The Next Point is Null");
            return nextPoint;
        }

        public Point GetPrevKey()
        {
            if (prevPoint == null) Debug.LogError("The Previous Point is Null");
            return prevPoint;
        }

        public bool IsCenter()
        {
            return isCenter;
        }

        public bool JustPassedCenter()
        {
            return justPassedCenter;
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
