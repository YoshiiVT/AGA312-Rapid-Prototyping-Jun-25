using UnityEngine;

namespace PROTOTYPE_1
{
    public class MenuCameraSpin : MonoBehaviour
    {
        public float rotationSpeed;

        void Update()
        {
            transform.Rotate(Vector3.up, 1 * rotationSpeed * Time.deltaTime);
        }
    }
}
