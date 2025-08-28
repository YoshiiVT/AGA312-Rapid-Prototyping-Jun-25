using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace PROTOTYPE_5
{
    public class PlayerLook : MonoBehaviour
    {
        [SerializeField] Camera cam;
        [SerializeField,ReadOnly] private float xRotation = 0f;

        [SerializeField] private float xSensitivity = 30f;
        [SerializeField] private float ySensitivity = 30f;

        private void Awake()
        {
            if (cam == null) { Debug.LogError("Camera returned NULL"); }
        }

        public void ProcessLook(Vector2 input)
        {
            float mouseX = input.x;
            float mouseY = input.y;
            //calculate camera roation for looking up and down
            xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
            xRotation = Mathf.Clamp(xRotation, -80f, 80f);
            //apply this to our camera transform
            cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //rotate player to look left and right
            transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);

        }
    }
}

