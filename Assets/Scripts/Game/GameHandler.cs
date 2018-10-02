using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {

    public ScoreManager scoreManager;
    public AsteroidSpawner asteroidSpawner;
    public TimeManager timeManager;
    public SpaceCraft spaceCraft;
    public GameUIManager gameUI;

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
        asteroidSpawner.AsteroidDestroyedAction += AsteroidDestroyedCallBack;
        timeManager.StartTimer();
        gameState = GameStateEnum.Run;
        spaceCraft.enabled = true;
    }
    private void OnDestroy()
    {
        asteroidSpawner.AsteroidDestroyedAction -= AsteroidDestroyedCallBack;
    }
    private void AsteroidDestroyedCallBack(Asteroid obj)
    {
        scoreManager.DestoryAsteroid();
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

}
