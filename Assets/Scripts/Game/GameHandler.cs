using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    public AsteroidSpawner asteroidSpawner;
    public TimeManager timeManager;
    public SpaceCraft spaceCraft;
    public GameUIManager gameUI;
    #region Singleton
    private static GameHandler instance;
    public static GameHandler Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameHandler>();
            }
            return instance;
        }
    }
    #endregion Singleton
    public enum GameStateEnum
    {
        Menu,
        Pause,
        Run
    }

    private GameStateEnum gameState;
    public GameStateEnum GameState
    {
        get { return gameState; }
    }

    public void StartNewGame()
    {
        Time.timeScale = 1;
        asteroidSpawner.StartSpawning();
        spaceCraft.ZeroHealthAction += GameOver;
        timeManager.StartTimer();
        gameState = GameStateEnum.Run;
        spaceCraft.enabled = true;
    }
    private void OnDestroy()
    {
        spaceCraft.ZeroHealthAction -= GameOver;
    }
   
    public void ExitTheGame()
    {
        Application.Quit();
    }
    public void PauseTheGame()
    {
        Time.timeScale = 0;
        gameState = GameStateEnum.Pause;
        spaceCraft.enabled = false;

    }
    public void ResumeTheGame()
    {
        Time.timeScale = 1;
        gameState = GameStateEnum.Run;
        spaceCraft.enabled = true;
    }
    public void RestartTheGame()
    {
        SceneManager.LoadScene("Main");
    }
    public void GameOver()
    {
        PauseTheGame();
    }

}
