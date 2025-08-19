using UnityEngine;

namespace PROTOTYPE_5
{
    public class FPController : GameBehaviour<FPController>
    {

        [Header("Movement")]
        [SerializeField, ReadOnly] private float moveSpeed;
        [SerializeField] private float walkSpeed;
        [SerializeField] private float sprintSpeed;

        [SerializeField] private float groundDrag;

        [Header("Jumping")]
        [SerializeField] private float jumpForce;
        [SerializeField] private float jumpCooldown;
        [SerializeField] private float airMultiplier;
        [SerializeField, ReadOnly] bool readyToJump;

        [Header("Keybinds")]
        public KeyCode jumpKey = KeyCode.Space;
        public KeyCode sprintKey = KeyCode.LeftShift;

        [Header("Ground Check")]
        [SerializeField] private float playerHeight;
        [SerializeField] private LayerMask whatIsGround; //Baby dont hurt me
        [SerializeField, ReadOnly] bool grounded;

        public Transform orientation;

        [SerializeField, ReadOnly] private float horizontalInput;
        [SerializeField, ReadOnly] private float verticalInput;

        [SerializeField, ReadOnly] private Vector3 moveDirection;

        [SerializeField, ReadOnly] private Rigidbody rb;

        [SerializeField, ReadOnly] MovementState state;
        public enum MovementState { walking, sprinting, air}

        private void Awake()
        {
            Instantiate();
        }

        private void Start()
        {
            if (orientation == null) { Debug.LogError("Orientation returned NULL"); }

            rb = GetComponent<Rigidbody>();
            if (rb == null) { Debug.LogError("RigidBody returned NULL"); }
            rb.freezeRotation = true;

            readyToJump = true;
        }

        private void Update()
        {
            // ground check
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

            MyInput();
            SpeedControl();
            StateHandler();

            // handle drag
            if (grounded)
                rb.linearDamping = groundDrag;
            else
                rb.linearDamping = 0;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            // when to jump
            if(Input.GetKey(jumpKey) && readyToJump && grounded)
            {
                readyToJump = false;

                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
        }

        private void StateHandler()
        {
            // Mode - Sprinting
            if (grounded && Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }

            // Mode - Walking
            else if (grounded)
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }

            // Mode - Air
            else
            {
                state = MovementState.air;
            }
        }

        private void MovePlayer()
        {
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

            // on ground
            if (grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);

            // in air
            else if (!grounded)
                rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            // limit velocity if needed
            if(flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            // reset y velocity
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            readyToJump = true;
        }
    }
}
