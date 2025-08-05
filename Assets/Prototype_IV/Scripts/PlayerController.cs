using UnityEngine;

namespace PROTOTYPE_4
{
    public class PlayerController : MonoBehaviour
    {
        //Temp
        private GameManager gameManager;

        public float velocity = 1;
        [SerializeField] private Rigidbody2D rb;

        private GameObject lastCollidedObj;

        [Header("Rotation Settings")]
        public float rotationSpeed = 2f; // How quickly the object rotates
        public float maxUpRotation = 0f;
        public float maxDownRotation = -180f;
        public float defaultRotation = -90f;

        private Vector3 lastPosition;
        private float currentRotation;

        void Start()
        {
            //Temp
            GameObject gameManagerobj = GameObject.Find("GameManager");
            gameManager = gameManagerobj.GetComponent<GameManager>();

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
                lastCollidedObj = collision.gameObject;
                gameManager.Death();
            }
            else { Debug.LogWarning("Something Detected"); }
        }

        public void DestroyLastCollidedObj()
        {
            GameObject lastCollidedObjParent = lastCollidedObj.transform.parent.gameObject;
            Destroy(lastCollidedObjParent);
        }

        void SetRotation(float zRotation)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, zRotation);
        }
    }

}
