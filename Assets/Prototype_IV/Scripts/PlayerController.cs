using UnityEngine;

namespace PROTOTYPE_4
{
    public class PlayerController : MonoBehaviour
    {
        public float velocity = 1;
        [SerializeField, ReadOnly] private Rigidbody2D rb;


        [Header("Rotation Settings")]
        public float rotationSpeed = 2f; // How quickly the object rotates
        public float maxUpRotation = 0f;
        public float maxDownRotation = -180f;
        public float defaultRotation = -90f;

        private Vector3 lastPosition;
        private float currentRotation;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) { Debug.LogError("Rigidbody NOT found"); }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Jump
                rb.linearVelocity = Vector2.up * velocity;
            }

            #region tempRotationLogic
            float verticalMovement = transform.position.y - lastPosition.y;

            float targetRotation = defaultRotation;

            if (verticalMovement > 0f)
            {
                // Moving up: rotate toward 0
                targetRotation = Mathf.Lerp(currentRotation, maxUpRotation, Time.deltaTime * rotationSpeed);
            }
            else if (verticalMovement < 0f)
            {
                // Moving down: rotate toward -180
                targetRotation = Mathf.Lerp(currentRotation, maxDownRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                // No movement: slowly return to default
                targetRotation = Mathf.Lerp(currentRotation, defaultRotation, Time.deltaTime * rotationSpeed);
            }

            // Clamp rotation between maxDownRotation and maxUpRotation
            currentRotation = Mathf.Clamp(targetRotation, maxDownRotation, maxUpRotation);
            SetRotation(currentRotation);

            lastPosition = transform.position;
            #endregion
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Collision"))
            {
                Debug.Log("Collision Detected");
                Time.timeScale = 0;
            }
            else { Debug.LogWarning("Something Detected"); }
        }

        void SetRotation(float zRotation)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
        }
    }

}
