using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    private InputActions inputActions;
    public event EventHandler onInteractionEvent;
    public event EventHandler onInteractionAlternateEvent;
    public static event EventHandler onGamePauseEvent;
    public static void ClearStaticData()
    {
        onGamePauseEvent = null;
    }
    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        inputActions.Player.PauseGame.performed += PauseGame_performed;
    }
    private void PauseGame_performed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
        onGamePauseEvent?.Invoke(this, EventArgs.Empty);
    }
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onInteractionAlternateEvent?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        onInteractionEvent?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetNormalizedInput => inputActions.Player.Move.ReadValue<Vector2>().normalized;
}
