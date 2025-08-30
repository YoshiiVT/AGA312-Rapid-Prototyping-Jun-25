using UnityEngine;
using StateMachine;

public class ProximityTargetGetterBehaviour : StateTransitionCondition
{
    [SerializeField] private Transform target;
    [SerializeField] private float range = 10f;

    public override bool IsMet()
    {
        if (target == null) return false;
        return Vector3.Distance(transform.position, target.position) <= range;
    }

}
