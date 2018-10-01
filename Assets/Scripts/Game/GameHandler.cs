using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public ScoreManager scoreManager;
    public AsteroidSpawner asteroidSpawner;
    public TimeManager timeManager;

    private void Start()
    {
        asteroidSpawner.AsteroidDestroyedAction += AsteroidDestroyedCallBack;
        timeManager.StartTimer();
    }
    private void OnDestroy()
    {
        asteroidSpawner.AsteroidDestroyedAction -= AsteroidDestroyedCallBack;
    }
    private void AsteroidDestroyedCallBack(Asteroid obj)
    {
        scoreManager.DestoryAsteroid();
    }
}
