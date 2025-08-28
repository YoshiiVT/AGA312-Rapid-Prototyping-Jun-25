using UnityEngine;

namespace PROTOTYPE_5
{
    public class PlayerMotor : MonoBehaviour
    {
        [Header("Player Movement References")]
        [SerializeField, ReadOnly] CharacterController controller;
        [SerializeField, ReadOnly] Vector3 playerVelocity;

        [Header("Jump and Gravity Variables")]
        [SerializeField, ReadOnly] bool isGrounded;

        [Header("Customisable Variables")]
        [SerializeField] float speed = 5f;
        [SerializeField] float gravity = -9.8f;
        [SerializeField] float jumpHeight = 3f;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            if (controller == null) { Debug.LogError("Character Controller returned NULL"); }
        }

        private void Update()
        {
            isGrounded = controller.isGrounded;
        }

        public void ProcessMove(Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

            playerVelocity.y += gravity * Time.deltaTime;

            if (isGrounded && playerVelocity.y < 0) { playerVelocity.y = -2; }

            controller.Move(playerVelocity * Time.deltaTime);
            Debug.Log(playerVelocity);
        }

        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }
    }
}

