using System.Collections.Generic;
using UnityEngine;

namespace PROTOTYPE_5
{
    public class PlayerMotor : GameBehaviour
    {
        [Header("Player Movement References")]
        [SerializeField, ReadOnly] CharacterController controller;
        [SerializeField, ReadOnly] Vector3 playerVelocity;

        [Header("Audio")]
        [SerializeField,ReadOnly] AudioSource playerAudioSource;
        [SerializeField] List<AudioClip> footsteps;

        [Header("Footstep Settings")]
        [SerializeField,ReadOnly] private bool isMoving;

        [SerializeField] private float stepInterval = 0.5f; // time between steps
        private float stepCooldown = 0f;
        private int lastFootstepIndex = -1; // track last played sound


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

            playerAudioSource = GetComponent<AudioSource>();
            if (controller == null) { Debug.LogError("Audio Source returned NULL"); }
        }

        private void Update()
        {
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
            // update grounded state at the start of movement processing
            isGrounded = controller.isGrounded;

            // mark moving based on input magnitude and grounded state
            isMoving = input.sqrMagnitude > 0.0001f && isGrounded;

            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;
            controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

            playerVelocity.y += gravity * Time.deltaTime;

            if (isGrounded && playerVelocity.y < 0) { playerVelocity.y = -2; }

            controller.Move(playerVelocity * Time.deltaTime);

            HandleFootsteps();
        }

        private void HandleFootsteps()
        {
            if (!isMoving || footsteps == null || footsteps.Count == 0 || playerAudioSource == null)
            {
                stepCooldown = Mathf.Min(stepCooldown, stepInterval);
                return;
            }

            stepCooldown -= Time.deltaTime;
            if (stepCooldown <= 0f)
            {
                int index;

                // reroll until it's different (safe for >1 clips)
                do
                {
                    index = Random.Range(0, footsteps.Count);
                }
                while (index == lastFootstepIndex && footsteps.Count > 1);

                playerAudioSource.PlayOneShot(footsteps[index]);
                lastFootstepIndex = index;

                // reset cooldown with sprint/crouch adjustments
                float interval = stepInterval;
                if (sprinting) interval *= 0.6f;
                if (crouching) interval *= 1.4f;

                stepCooldown = interval;
            }
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
            if (!isGrounded && sprinting)
            {
                ExecuteAfterSeconds(0.1f, () =>
                {
                    Sprint();
                });
            }
        }
    }
}

