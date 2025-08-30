using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAimingBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretBase;  // rotates horizontally (Y)
    [SerializeField] private Transform turretHead;  // rotates vertically (X)
    [SerializeField] private Transform target;

    [SerializeField] private List<FiringPoint> firingPoints = new List<FiringPoint>();

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 90f; // degrees/sec
    [SerializeField] private float aimFault = 5f;      // degrees tolerance

    [Header("Output")]
    [SerializeField] private bool lockedOn = false;

    private void OnEnable()
    {
        StartCoroutine(AutoFire());
    }

    private void OnDisable()
    {
        StopAllCoroutines(); // safer
    }

    private void Update()
    {
        if (target == null || turretBase == null || turretHead == null)
        {
            lockedOn = false;
            return;
        }

        // --- Step 1: Calculate the direction vector to the target ---
        Vector3 targetDir = target.position - turretBase.position;

        // --- Step 2: Rotate turret base (yaw) ---
        Vector3 baseDir = new Vector3(targetDir.x, 0f, targetDir.z); // project onto horizontal plane
        if (baseDir.sqrMagnitude > 0.001f)
        {
            Quaternion desiredBaseRotation = Quaternion.LookRotation(baseDir);
            turretBase.rotation = Quaternion.RotateTowards(
                turretBase.rotation,
                desiredBaseRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // --- Step 3: Rotate turret head (pitch only) ---

        // Direction from head to target, in head’s local space
        Vector3 localTargetDir = turretHead.parent.InverseTransformPoint(target.position);

        // Desired pitch angle (X axis)
        float desiredPitch = Mathf.Atan2(localTargetDir.y, localTargetDir.z) * Mathf.Rad2Deg;

        // Build a rotation that tilts only around X
        Quaternion desiredHeadRotation = Quaternion.Euler(desiredPitch, 0f, 0f);

        // Smoothly rotate head towards desired pitch
        turretHead.localRotation = Quaternion.RotateTowards(
            turretHead.localRotation,
            desiredHeadRotation,
            rotationSpeed * Time.deltaTime
        );

        // --- Step 4: Check if we are aligned (locked-on) ---
        Vector3 forwardDir = turretHead.forward;
        Vector3 aimDir = (target.position - turretHead.position).normalized;

        float angleToTarget = Vector3.Angle(forwardDir, aimDir);
        lockedOn = angleToTarget <= aimFault;
    }

    private IEnumerator AutoFire()
    {
        while (this.enabled)
        {
            while (lockedOn)
            {
                foreach (FiringPoint _FP in firingPoints)
                {
                    _FP.Fire();
                }

                yield return new WaitForSeconds(0.2f);
            }

            yield return null;
        }
    }
}
