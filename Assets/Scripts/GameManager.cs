using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    public event EventHandler OnStateChanged;
    private bool isGamePause = false;
    public static GameManager Instance { get; private set; }
    public enum GameState
    {
        WaitingToStart,
        CountDownToStart,
        GameStart,
        GameOver
    }
    private NetworkVariable<GameState> state = new NetworkVariable<GameState>(GameState.WaitingToStart);
    private float waitingToStart = 1f;
    private float countDownToStart = 3f;
    private NetworkVariable<float> gamePlayingTimer = new NetworkVariable<float>(0f);
    private NetworkVariable<float> gamePlayingTimerMax = new NetworkVariable<float>(300f);
    private void Awake()
    {
        if (Instance == null) Instance = this;

    }
    void Update()
    {
        if (!IsServer) return;

        switch (state.Value)
        {
            case GameState.WaitingToStart:
                if (waitingToStart < 0f)
                {

                    state.Value = GameState.CountDownToStart;
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
                    gamePlayingTimer.Value = gamePlayingTimerMax.Value;
                    state.Value = GameState.GameStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    countDownToStart -= Time.deltaTime;
                }
                break;
            case GameState.GameStart:
                if (gamePlayingTimer.Value < 0f)
                {

                    state.Value = GameState.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    gamePlayingTimer.Value -= Time.deltaTime;
                }
                break;
            case GameState.GameOver:

                break;
        }
    }
    public void setTimer()
    {
        gamePlayingTimer.Value = gamePlayingTimerMax.Value;
    }
    public GameState GetCurrentGameState()
    {
        return state.Value;
    }
    public bool IsCountDownActive()
    {
        return state.Value == GameState.CountDownToStart;
    }
    public float GetCountDownTimer() => countDownToStart;
    public bool IsGameOver()
    {
        return state.Value == GameState.GameOver;
    }
    public float GetRecipeTimeout()
    {
        return 1 - (gamePlayingTimer.Value / gamePlayingTimerMax.Value);
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
