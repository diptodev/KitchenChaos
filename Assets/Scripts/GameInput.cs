using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private InputActions inputActions;
    public event EventHandler onInteractionEvent;
    public event EventHandler onInteractionAlternateEvent;
    private void Awake()
    {
        inputActions=new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
      onInteractionAlternateEvent?.Invoke(this,EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onInteractionEvent?.Invoke(this,EventArgs.Empty);
    }

    public Vector2 GetNormalizedInput=> inputActions.Player.Move.ReadValue<Vector2>().normalized;
}
