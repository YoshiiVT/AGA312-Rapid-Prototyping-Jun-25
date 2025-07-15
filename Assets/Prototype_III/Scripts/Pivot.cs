using UnityEngine;

public class Pivot : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;

    void Update()
    {
        float zRotation = transform.eulerAngles.z;

        // Adjust for Unity's 0-360 angle wrapping
        if (zRotation > 180) zRotation -= 360;

        if (Input.GetKey(KeyCode.A) && zRotation < 50f)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D) && zRotation > -50f)
        {
            transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        }
    }
}
