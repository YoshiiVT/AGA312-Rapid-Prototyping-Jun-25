using UnityEngine;
using System.Collections.Generic;
using System.Collections;
 using UnityEngine.InputSystem;
using System;

public class InputManager : GameBehaviour
{
   #region Variables
    public InputActionAsset inputActions;
    private InputAction moveAction, jumpAction;
   #endregion Variables

   #region Unity Functions
   private void Start()
   {
   
   }
   private void Update()
   {
        Vector2 move = moveAction.ReadValue<Vector2>();
        OnMove?.Invoke(move);
   }
    #endregion Unity Functions

    #region Public Functions

    #endregion Public Functions

    #region Private Functions
    private void Awake()
    {
        moveAction = inputActions.FindActionMap("Player").FindAction("Move");

        inputActions.FindActionMap("Player").FindAction("Jump").performed += (context) =>
        {
            if (context.action.WasPressedThisFrame())
                OnJump?.Invoke();
        };

        inputActions.FindActionMap("Player").FindAction("Attack").performed += (context) =>
        {
            if (context.action.WasPressedThisFrame())
                OnAttack?.Invoke();
        };
    }
    #endregion Private Functions

    #region Events
    //Setup
    public static Action<Vector2> OnMove = null;
    public static Action OnJump = null;
    public static Action OnAttack = null;
    #endregion Events
}

