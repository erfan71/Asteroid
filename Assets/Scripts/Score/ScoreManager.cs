using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int destroyingAsteroidScore;
    public ScoreHUDManager scoreHUD;
    public AsteroidSpawner asteroidSpawner;

    private int currentScore;
    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
        }
    }

    private void Start()
    {
        asteroidSpawner.AsteroidDestroyedAction += AsteroidDestroyedCallBack;
    }
    private void OnDestroy()
    {
        asteroidSpawner.AsteroidDestroyedAction -= AsteroidDestroyedCallBack;
    }

    public void DestoryAsteroid()
    {
        CurrentScore += destroyingAsteroidScore;
        scoreHUD.UpdateScore(CurrentScore);
    }
    private void AsteroidDestroyedCallBack(Asteroid obj)
    {
        DestoryAsteroid();
    }
}
