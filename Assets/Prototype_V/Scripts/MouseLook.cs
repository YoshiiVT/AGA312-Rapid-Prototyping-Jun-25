using UnityEngine;

namespace PROTOTYPE_5
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 100f;

        [SerializeField] private Transform orientation;

        [SerializeField, ReadOnly] private float xRotation = 0f;
        [SerializeField, ReadOnly] private float yRotation = 0f;

        void Start()
        {
            if (orientation == null) { Debug.LogError("Orientation is NULL"); }

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            // get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -85f, 85f);
            
            // rotate cam and orientation
            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
