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

        [Header("Sprint Variables")]
        [SerializeField, ReadOnly] private bool sprinting;

        [Header("Crouch Variables")]
        [SerializeField, ReadOnly] private bool lerpCrouch;
        [SerializeField, ReadOnly] private bool crouching;
        [SerializeField] private float crouchTimer;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
            if (controller == null) { Debug.LogError("Character Controller returned NULL"); }
        }

        private void Update()
        {
            isGrounded = controller.isGrounded;

            //Crouch Logic

            if (lerpCrouch) //Nani da fuk is happening here?
            {
                crouchTimer += Time.deltaTime;
                float p = crouchTimer / 1;
                p *= p;
                if (crouching) { controller.height = Mathf.Lerp(controller.height, 1, p); }
                else { controller.height = Mathf.Lerp(controller.height, 2, p); }

                if (p > 1)
                {
                    lerpCrouch = false;
                    crouchTimer = 0f;
                }
            }
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
            //Debug.Log(playerVelocity);
        }

        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
            }
        }

        public void Crouch() 
        {
            if (isGrounded && !sprinting)
            {
                crouching = !crouching;
                crouchTimer = 0;
                lerpCrouch = true;
                
                if (crouching) { speed = 2.5f; }
                else { speed = 5; }
            }
        }

        public void Sprint()
        {
            if (isGrounded && !crouching)
            {
                sprinting = !sprinting;
                if (sprinting) { speed = 8; }
                else { speed = 5; } //Don't like how this is HardCoded!! >_<
            }
        }
    }
}

