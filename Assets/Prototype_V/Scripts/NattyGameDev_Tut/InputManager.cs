using UnityEngine;
using UnityEngine.InputSystem;

namespace PROTOTYPE_5
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField, ReadOnly] private PlayerInput playerInput;
        [SerializeField, ReadOnly] private PlayerInput.OnFootActions onFoot;
        public PlayerInput.OnFootActions OnFoot => onFoot;

        [SerializeField, ReadOnly] private PlayerMotor motor;
        [SerializeField, ReadOnly] private PlayerLook look;
        [SerializeField, ReadOnly] private PlayerWeapon weapon;

        private void Awake()
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;

            motor = GetComponent<PlayerMotor>();
            if (motor == null) { Debug.LogError("Player Motor returned NULL"); }



            look = GetComponent<PlayerLook>();
            if (look == null) { Debug.LogError("Player Look returned NULL"); }

            weapon = GetComponent<PlayerWeapon>();
            if (weapon == null) { Debug.LogError("Player Weapon returned NULL"); }

            //Button Event Logic
            onFoot.Jump.performed += ctx => motor.Jump();
            onFoot.Crouch.performed += ctx => motor.Crouch();
            onFoot.Sprint.performed += ctx => motor.Sprint();
            OnFoot.Fire.performed += ctx => weapon.FireWeapon();
        }

        private void Update()
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
