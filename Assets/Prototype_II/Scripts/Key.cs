using UnityEngine;

namespace PROTOTYPE_2
{
    public class Key : MonoBehaviour
    {
        [SerializeField, ReadOnly] private Key nextKey;
        [SerializeField, ReadOnly] private Key prevKey;
        [SerializeField] private bool isEnd;
        [SerializeField] private bool isStart;

        public Key GetNextKey()
        {
            if (nextKey == null) Debug.LogError("The Next Key is Null");
            return nextKey;
        }

        public Key GetPrevKey()
        {
            if (prevKey == null) Debug.LogError("The Previous Key is Null");
            return prevKey;
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
