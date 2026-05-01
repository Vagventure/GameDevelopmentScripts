using System;
using Unity.VisualScripting;
using UnityEngine;

public class CakeSimGameObject : MonoBehaviour
{
    public static CakeSimGameObject Instance { get; private set; }

    public event EventHandler OnStateChange;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;
    public enum State
    {
        WaitingToStart,
        CountDownToStart,
        GameStart,
        GameOver
    }

    private State state;
    private float waitingToStartTimer = 5f;
    private float countDownTimer = 3f;
    private float gamePlayingTimer;
    private float maxGamePlayTimeMax = 60f;
    private bool isGamePaused;

    private void Awake()
    {
        state = State.WaitingToStart;
        Instance = this;
    }

    private void Start()
    {
        PlayerInput.Instance.OnPauseperformed += PlayerInput_OnPauseperformed;
    }

    private void PlayerInput_OnPauseperformed(object sender, EventArgs e)
    {
        TogglePause();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    state = State.CountDownToStart;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.CountDownToStart:
                countDownTimer -= Time.deltaTime;
                if(countDownTimer < 0f)
                {
                    state = State.GameStart;
                    gamePlayingTimer = maxGamePlayTimeMax;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameStart:
                gamePlayingTimer -= Time.deltaTime;
                if(gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }

        Debug.Log(state);
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }

    public bool IsGamePlaying()
    {
        return state == State.GameStart;
    }
   
    public bool IsCountDownTimerOn()
    {
        return state == State.CountDownToStart;
    }
  
    public float GetGamePlayTimeNormaliazed()
    {
        return 1 - (gamePlayingTimer / maxGamePlayTimeMax);
    }

    public float GetCountDownTimer()
    {
        return countDownTimer;
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;

            OnGamePause?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameResume?.Invoke(this, EventArgs.Empty);
        }
    }

}