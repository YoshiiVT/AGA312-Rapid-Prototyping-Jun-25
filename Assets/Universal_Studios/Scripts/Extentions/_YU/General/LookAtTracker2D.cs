using UnityEngine;

public class LookAtTracker2D : MonoBehaviour
{
    private GameObject looker;
    private GameObject target;
    void Update()
    {
      Vector3 look =  transform.InverseTransformPoint(target.transform.position);
      float Angle = Mathf.Atan2(look.y , look.x) * Mathf.Rad2Deg;

      looker.transform.Rotate (0, 0, Angle);
    }
}
