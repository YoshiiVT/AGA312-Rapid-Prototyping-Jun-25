using UnityEngine;

public class TurretIdleScan : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("The part of the turret that rotates (e.g. the head)")]
    [SerializeField] private Transform turret;

    [Tooltip("Maximum angle left/right relative to the starting rotation")]
    [SerializeField] private float scanAngle = 45f;

    [Tooltip("Degrees per second rotation speed")]
    [SerializeField] private float rotationSpeed = 30f;

    private float centerYaw;   // fixed reference yaw
    private int direction = 1; // 1 = right, -1 = left

    private void Awake()
    {
        // Set the scan center based on initial placement of the turret
        if (turret != null)
            centerYaw = turret.localEulerAngles.y;
    }

    private void Update()
    {
        if (turret == null) return;

        // Rotate continuously
        turret.Rotate(Vector3.up, rotationSpeed * direction * Time.deltaTime, Space.Self);

        // Current yaw relative to center
        float offset = Mathf.DeltaAngle(centerYaw, turret.localEulerAngles.y);

        // Flip direction if we’ve reached the scan limit
        if (Mathf.Abs(offset) >= scanAngle)
        {
            direction *= -1;
        }
    }
}
