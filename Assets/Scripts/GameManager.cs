using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
    public class OnStateChangedValue : EventArgs
    {
        public GameState gameState;
    }
    private bool isGamePause = false;
    public static GameManager Instance { get; private set; }
    public enum GameState
    {
        WaitingToStart,
        CountDownToStart,
        GameStart,
        GameOver,
        Default
    }
    //private NetworkVariable<GameState> state = new NetworkVariable<GameState>(GameState.WaitingToStart);
    private GameState state = GameState.WaitingToStart;
    private float waitingToStart = 1f;
    private float countDownToStart = 3f;
    private float gamePlayingTimer = (0f);
    private float gamePlayingTimerMax = 30f;
    // public override void OnNetworkSpawn()
    // {
    //     //  state.OnValueChanged += GameStateValueChanged;
    // }
    // private void GameStateValueChanged(GameState prevState, GameState newState)
    // {
    //     OnStateChanged?.Invoke(this, new OnStateChangedValue
    //     {
    //         gameState = newState
    //     });
    // }
    private void Awake()
    {
        if (Instance == null) Instance = this;

    }
    void Update()
    {
        // if (!IsServer) return;

        switch (state)
        {
            case GameState.WaitingToStart:
                if (waitingToStart < 0f)
                {

                    state = GameState.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    waitingToStart -= Time.deltaTime;
                }
                break;
            case GameState.CountDownToStart:
                if (countDownToStart < 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMax;
                    state = GameState.GameStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    countDownToStart -= Time.deltaTime;
                }
                break;
            case GameState.GameStart:
                if (gamePlayingTimer < 0f)
                {
                    state = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    gamePlayingTimer -= Time.deltaTime;
                }
                break;
            case GameState.GameOver:

                break;
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void GameStateChangedServerRpc()
    {
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    [ClientRpc]
    private void GameStateChangedClientRpc()
    {
        OnStateChanged?.Invoke(this, new OnStateChangedValue
        {
            gameState = state
        });

    }
    public void setTimer()
    {
        gamePlayingTimer = gamePlayingTimerMax;
    }
    public GameState GetCurrentGameState()
    {
        return state;
    }
    public bool IsCountDownActive()
    {
        return state == GameState.CountDownToStart;
    }
    public float GetCountDownTimer() => countDownToStart;
    public bool IsGameOver()
    {
        return state == GameState.GameOver;
    }
    public float GetRecipeTimeout()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }
    public void TooglePauseGame()
    {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public bool IsGamePlaying()
    {
        return Time.timeScale == 1f;
    }
}
