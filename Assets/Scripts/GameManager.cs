using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : NetworkBehaviour
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
    private NetworkVariable<GameState> state = new NetworkVariable<GameState>(GameState.WaitingToStart);
    // private GameState state = GameState.WaitingToStart;
    // private float waitingToStart = 1f;
    private bool isLocalPlayerReady = false;
    private NetworkVariable<float> countDownToStart = new NetworkVariable<float>(3f);

    private float gamePlayingTimerMax = 30f;
    private NetworkVariable<float> gamePlayingTimer = new NetworkVariable<float>(0f);
    private Dictionary<ulong, bool> connectedClientActiveStatus = new Dictionary<ulong, bool>();
    public override void OnNetworkSpawn()
    {
        state.OnValueChanged += GameStateValueChanged;
    }
    private void GameStateValueChanged(GameState prevState, GameState newState)
    {
        OnStateChanged?.Invoke(this, EventArgs.Empty);
    }
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    private void Start()
    {
        GameInput.instance.onPlayerReadyEvent += GameInput_OnPlayerReadyEvent;
    }
    private void GameInput_OnPlayerReadyEvent(object sender, System.EventArgs e)
    {
        if (state.Value == GameState.WaitingToStart)
        {
            isLocalPlayerReady = true;
            LocalPlayerReadyServerRpc();
        }
    }
    void Update()
    {
        if (!IsServer) return;

        switch (state.Value)
        {
            case GameState.WaitingToStart:

                break;
            case GameState.CountDownToStart:
                countDownToStart.Value -= Time.deltaTime;
                if (countDownToStart.Value < 0f)
                {
                    gamePlayingTimer.Value = gamePlayingTimerMax;
                    state.Value = GameState.GameStart;
                }

                break;
            case GameState.GameStart:
                gamePlayingTimer.Value -= Time.deltaTime;
                if (gamePlayingTimer.Value < 0f)
                {
                    state.Value = GameState.GameOver;
                }
                break;
            case GameState.GameOver:

                break;
        }
    }
    [ServerRpc(RequireOwnership = false)]
    private void LocalPlayerReadyServerRpc(ServerRpcParams serverRpcParams = default)
    {
        connectedClientActiveStatus[serverRpcParams.Receive.SenderClientId] = true;
        bool allClientIsReady = true;
        foreach (ulong clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            if (!connectedClientActiveStatus.ContainsKey(clientId) || !connectedClientActiveStatus[clientId])
            {
                allClientIsReady = false;
                break;
            }
        }
        if (allClientIsReady)
        {
            state.Value = GameState.CountDownToStart;
        }
    }

    public void setTimer()
    {
        gamePlayingTimer.Value = gamePlayingTimerMax;
    }
    public GameState GetCurrentGameState()
    {
        return state.Value;
    }
    public bool IsCountDownActive()
    {
        return state.Value == GameState.CountDownToStart;
    }
    public float GetCountDownTimer() => countDownToStart.Value;
    public bool IsGameOver()
    {
        return state.Value == GameState.GameOver;
    }
    public float GetRecipeTimeout()
    {
        return 1 - (gamePlayingTimer.Value / gamePlayingTimerMax);
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
    public bool IsGameStart()
    {
        return state.Value == GameState.GameStart;
    }
    public bool IsGamePlaying()
    {
        return Time.timeScale == 1f;
    }
    public bool IsGameWaitingToStart()
    {
        return state.Value == GameState.WaitingToStart;
    }
}
