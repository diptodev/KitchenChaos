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
    private const string PLAYER_PREFS_BINDING = "PlayerPrefsBinding";
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
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDING))
        {
            inputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDING));
        }
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
    public void ChangeKeyBinding(Binding binding, Action onRebindCallback)
    {
        int bindingIndex;
        InputAction inputAction;
        switch (binding)
        {
            default:
            case Binding.MoveUp:
                bindingIndex = 6;
                inputAction = inputActions.Player.Move;
                break;
            case Binding.MoveDown:
                bindingIndex = 7;
                inputAction = inputActions.Player.Move;
                break;
            case Binding.MoveLeft:
                bindingIndex = 8;
                inputAction = inputActions.Player.Move;
                break;
            case Binding.MoveRigt:
                bindingIndex = 9;
                inputAction = inputActions.Player.Move;
                break;
            case Binding.Interact:
                bindingIndex = 0;
                inputAction = inputActions.Player.Interact;
                break;
            case Binding.AlterInteract:
                bindingIndex = 0;
                inputAction = inputActions.Player.InteractAlternate;
                break;
            case Binding.Escape:
                bindingIndex = 0;
                inputAction = inputActions.Player.PauseGame;
                break;

        }
        inputActions.Player.Disable();
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete((callback) =>
        {
            inputActions.Player.Enable();
            onRebindCallback();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDING, inputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            inputActions.Dispose();
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
