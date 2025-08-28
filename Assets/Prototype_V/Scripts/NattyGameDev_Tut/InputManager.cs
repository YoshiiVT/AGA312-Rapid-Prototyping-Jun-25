using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PROTOTYPE_5
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private PlayerInput playerInput;
        [SerializeField, ReadOnly] private PlayerInput.OnFootActions onFoot;

        [SerializeField, ReadOnly] private PlayerMotor motor;
        [SerializeField, ReadOnly] private PlayerLook look;

        private void Awake()
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;

            motor = GetComponent<PlayerMotor>();
            if (motor == null) { Debug.LogError("Player Motor returned NULL"); }

            look = GetComponent<PlayerLook>();
            if (look == null) { Debug.LogError("Player Look returned NULL"); }

            //Button Event Logic
            onFoot.Jump.performed += ctx => motor.Jump();
        }

        private void FixedUpdate()
        {
            //Tell the playermotor to move using the value from our movement action
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }

        private void LateUpdate()
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            onFoot.Enable();
        }

        private void OnDisable()
        {
            onFoot.Disable();
        }

    }

}
