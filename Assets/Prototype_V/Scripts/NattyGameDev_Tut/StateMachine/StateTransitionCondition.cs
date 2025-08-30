using UnityEngine;

namespace StateMachine
{
    public abstract class StateTransitionCondition : MonoBehaviour
    {
        public abstract bool IsMet();
    }

}
