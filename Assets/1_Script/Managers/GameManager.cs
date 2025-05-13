using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public GameStatus gameStatus;
    public System.Action OnGameStart, OnGameSuccess, OnGameFail;
    [SerializeField] int fps = 60;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = fps;

        gameStatus = GameStatus.MENU;
    }

    public void Fail()
    {
        gameStatus = GameStatus.FAIL;
        OnGameFail?.Invoke();
    }

    public void Success()
    {
        gameStatus = GameStatus.SUCCESS;
        OnGameSuccess?.Invoke();
    }

    public void RestartLevel()
    {
        DOTween.KillAll();
        OnGameFail = null;
        OnGameStart = null;
        OnGameSuccess = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        gameStatus = GameStatus.PLAY;
        OnGameStart?.Invoke();
    }

    public enum GameStatus
    {
        MENU,
        PLAY,
        PAUSED,
        FAIL,
        SUCCESS
    }
}