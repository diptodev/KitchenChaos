using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private InputActions inputActions;
    private void Awake()
    {
        inputActions=new InputActions();
        inputActions.Player.Enable();
    }
    public Vector2 GetNormalizedInput=> inputActions.Player.Move.ReadValue<Vector2>().normalized;
}
