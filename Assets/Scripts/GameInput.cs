using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput instance { get; private set; }
    private InputActions inputActions;
    public event EventHandler onInteractionEvent;
    public event EventHandler onInteractionAlternateEvent;
    public static event EventHandler onGamePauseEvent;
    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRigt,
        Interact,
        AlterInteract,
        Escape
    }
    public static void ClearStaticData()
    {
        onGamePauseEvent = null;
    }
    private void Awake()
    {
        instance = this;
        inputActions = new InputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        inputActions.Player.PauseGame.performed += PauseGame_performed;
    }
    private void PauseGame_performed(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
        onGamePauseEvent?.Invoke(this, EventArgs.Empty);
        Debug.Log("Preformed");
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
    private void OnDestroy()
    {
        inputActions.Player.Interact.performed -= Interact_performed;
        inputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        inputActions.Player.PauseGame.performed -= PauseGame_performed;
        inputActions.Dispose();
    }
    public void ChangeKeyBinding(Binding binding, Action onReboundHide)
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.PerformInteractiveRebinding(6).OnComplete((callback) =>
        {
            inputActions.Player.Enable();
            onReboundHide();
        }).Start();
    }
    public string GetKeyBinding(Binding binding)
    {
        switch (binding)
        {
            case Binding.MoveUp:
                return inputActions.Player.Move.GetBindingDisplayString(6);

            case Binding.MoveDown:
                return inputActions.Player.Move.GetBindingDisplayString(7);
            case Binding.MoveLeft:
                return inputActions.Player.Move.GetBindingDisplayString(8);
            case Binding.MoveRigt:
                return inputActions.Player.Move.GetBindingDisplayString(9);
            case Binding.Interact:
                return inputActions.Player.Interact.GetBindingDisplayString(0);
            case Binding.AlterInteract:
                return inputActions.Player.InteractAlternate.GetBindingDisplayString(0);
            case Binding.Escape:
                return inputActions.Player.PauseGame.GetBindingDisplayString(0);
            default: return "";
        }
    }
}
