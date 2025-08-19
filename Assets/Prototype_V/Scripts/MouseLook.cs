using UnityEngine;

namespace PROTOTYPE_5
{
    public class MouseLook : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity = 100f;

        [SerializeField] private Transform playerBody;

        [SerializeField, ReadOnly] private float xRotation = 0f;

        void Start()
        {
            playerBody = GetComponentInParent<Transform>().parent;
            if (playerBody == null) { Debug.LogError("Player Body is NULL"); }

            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -85f, 85f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
