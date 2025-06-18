using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class RigidBodyX
{
    /// <summary>
    /// Returns the current speed (velocity) of a RigidBody
    /// </summary>
    /// <param name="_rb"> Needs the rigidbody called in to work</param>
    /// <returns></returns>
    static public float GetSpeedRB(Rigidbody _rb)
    {
        float speed = _rb.linearVelocity.magnitude;
        return speed;
    }

    static public bool IsStillMovingRB(Rigidbody _rb)
    {
        if (_rb.linearVelocity.sqrMagnitude > 0.01f || _rb.angularVelocity.sqrMagnitude > 0.01f)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// Forces a moving Rigidbody to filly stop
    /// </summary>
    /// <param name="_rb"></param>
    /// <param name="_waitTime"></param>
    /// <returns></returns>
    static public void ForceStopRB(Rigidbody _rb)
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.Sleep(); // Optional: forces the rigidbody to sleep (completely idle)
    }

    /// <summary>
    /// Gradually forces a rigidbody to stop within the duration.
    /// </summary>
    /// <param name="_rb"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    static public IEnumerator ForceStopGraduallyRB(Rigidbody _rb, float duration = 1f)
    {
        Vector3 initialVelocity = _rb.linearVelocity;
        Vector3 initialAngularVelocity = _rb.angularVelocity;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            _rb.linearVelocity = Vector3.MoveTowards(
                _rb.linearVelocity,
                Vector3.zero,
                Time.deltaTime * (_rb.linearVelocity.magnitude / duration)
            );

            _rb.angularVelocity = Vector3.MoveTowards(
                _rb.angularVelocity,
                Vector3.zero,
                Time.deltaTime * (_rb.angularVelocity.magnitude / duration)
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully stopped at the end
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.Sleep(); // optional
    }

}
