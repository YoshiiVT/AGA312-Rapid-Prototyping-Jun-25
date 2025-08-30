using UnityEngine;
using StateMachine;

public class ProximityCondition : StateTransitionCondition
{
    public enum CheckType { WithinRange, OutsideRange }

    [Header("References")]
    [SerializeField] private Transform sight;
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private float range = 10f;
    [SerializeField] private CheckType checkType = CheckType.WithinRange;
    [SerializeField] private LayerMask obstructionMask = ~0; // default: everything

    public override bool IsMet()
    {
        if (target == null || sight == null)
            return false;

        // --- Step 1: Check distance ---
        float distance = Vector3.Distance(transform.position, target.position);
        bool inRange = distance <= range;

        // --- Step 2: Check line of sight (raycast) ---
        Vector3 dir = (target.position - sight.position).normalized;

        if (Physics.Raycast(sight.position, dir, out RaycastHit hit, range, obstructionMask))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                // Something else is blocking the view
                return false;
            }
        }
        else
        {
            // Nothing was hit in range
            return false;
        }

        // --- Step 3: Apply range check type ---
        switch (checkType)
        {
            case CheckType.WithinRange:
                return inRange;
            case CheckType.OutsideRange:
                return !inRange;
            default:
                return false;
        }
    }
}

