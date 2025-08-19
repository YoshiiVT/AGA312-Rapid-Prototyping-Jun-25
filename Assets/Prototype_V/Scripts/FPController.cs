using UnityEngine;

namespace PROTOTYPE_5
{
    public class FPController : GameBehaviour<FPController>
    {

        [Header("Movement")]

        [SerializeField] private float speed = 12f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float jumpHeight = 3;

        [SerializeField, ReadOnly] private Vector3 velocity;

        [SerializeField] private Transform groundCheck;
        [SerializeField, ReadOnly] private float groundDistance = 0.4f;
        [SerializeField] LayerMask groundMask;

        [SerializeField, ReadOnly] private bool isGrounded;

        private void Awake()
        {
            if (!Instantiate()) return;
        }



        void Update()
        {

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0) { velocity.y = -2f; }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            //controller.Move(move * speed * Time.deltaTime);

            if(Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            //controller.Move(velocity * Time.deltaTime);
        }
    }

}
