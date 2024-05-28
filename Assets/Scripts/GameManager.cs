using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event EventHandler OnStateChanged;
   public static GameManager Instance { get; private set; }
    public enum GameState
    {
        WaitingToStart,
        CountDownToStart,
        GameStart,
        GameOver
    }
    private GameState state=GameState.WaitingToStart;
    private float waitingToStart = 1f;
    private float countDownToStart = 3f;
    private float gamePlayingTimer = 10f;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        
    }
    void Start()
    {
        
    }

 
    void Update()
    {

      
      
        switch (state)
        {
            case GameState.WaitingToStart:
                Debug.Log("Waiting to Start");
                if (waitingToStart<0f)
                {
                    Debug.Log("CountDownToStart");
                    state = GameState.CountDownToStart;
                    OnStateChanged.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    waitingToStart -= Time.deltaTime;
                }
                break;
            case GameState.CountDownToStart:
                if (countDownToStart < 0f)
                {
                    Debug.Log("GameStart");
                    state = GameState.GameStart;
                    OnStateChanged.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    countDownToStart -= Time.deltaTime;
                }
                break;
            case GameState.GameStart:
                if (gamePlayingTimer < 0f)
                {
                    Debug.Log("GameOver");
                    state = GameState.GameOver;
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
    public void setTimer()
    {
        gamePlayingTimer=10;
    }
    public GameState GetCurrentGameState()
    {
        return state;
    }
    public bool IsCountDownActive()
    {
        return GameState.CountDownToStart == state;
    }
    public float GetCountDownTimer() => countDownToStart;
}
