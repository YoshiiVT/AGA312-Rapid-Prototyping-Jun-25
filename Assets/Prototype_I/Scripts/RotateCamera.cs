using UnityEngine;
using UnityEngine.InputSystem;

namespace PROTOTYPE_1
{
    public class RotateCamera : MonoBehaviour
    {
        public float rotationSpeed;
        public InputAction cameraControls;

        private void OnEnable()
        {
            cameraControls.Enable();
        }

        private void OnDisable()
        {
            cameraControls.Disable();
        }

        void Update()
        {
            float horizontalInput = cameraControls.ReadValue<float>(); //Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
        }
    }
}