using UnityEngine;
using StateMachine;

public class ProximityCondition : StateTransitionCondition
{
    public enum CheckType { WithinRange, OutsideRange }

    [SerializeField] private Transform target;
    [SerializeField] private float range = 10f;
    [SerializeField] private CheckType checkType = CheckType.WithinRange;

    public override bool IsMet()
    {
        if (target == null) return false;

        float distance = Vector3.Distance(transform.position, target.position);

        switch (checkType)
        {
            case CheckType.WithinRange:
                return distance <= range;
            case CheckType.OutsideRange:
                return distance > range;
            default:
                return false;
        }
    }
}
