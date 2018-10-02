using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public Button startButton;
    public Button restartButton;
    public Button resumeButton;
    public Button exitButton;

    public GameHandler gamehandler;
    public GameObject menuPanel;
    public GameObject upPanel;

    private static bool showtheMenu = true;

    void Start()
    {

        menuPanel.SetActive(true);
        upPanel.SetActive(false);
        startButton.onClick.AddListener(() => OnStartButtonClicked());
        resumeButton.onClick.AddListener(() => OnResumeButtonClicked());
        restartButton.onClick.AddListener(() => OnRestartButtonClicked());
        exitButton.onClick.AddListener(() => OnExitButtonClicked());

        restartButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);

        if (!showtheMenu)
        {
            OnStartButtonClicked();
        }
    }

    #region ButtonClickedCallBack
    private void OnExitButtonClicked()
    {
        gamehandler.ExitTheGame();
    }

    private void OnRestartButtonClicked()
    {
        RestartGame();
    }

    private void OnResumeButtonClicked()
    {
        ResumeGame();
    }

    void OnStartButtonClicked()
    {
        gamehandler.StartNewGame();
        menuPanel.gameObject.SetActive(false);

        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);

        upPanel.SetActive(true);


    }
    #endregion

    public void PauseGame()
    {
        menuPanel.gameObject.SetActive(true);
        
        gamehandler.PauseTheGame();
        upPanel.SetActive(false);

    }
    public void ResumeGame()
    {
        menuPanel.gameObject.SetActive(false);

        gamehandler.ResumeTheGame();
        upPanel.SetActive(true);

    }
    public void RestartGame()
    {
        showtheMenu = false;
        gamehandler.RestartTheGame();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gamehandler.GameState==GameHandler.GameStateEnum.Run)
                PauseGame();
            else if (gamehandler.GameState == GameHandler.GameStateEnum.Pause)
                ResumeGame();
            
        }
    }
}
