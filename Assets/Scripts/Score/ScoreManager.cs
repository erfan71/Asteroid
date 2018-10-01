using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    public int destroyingAsteroidScore;

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

   
    public void DestoryAsteroid()
    {
        CurrentScore += destroyingAsteroidScore;
        Debug.Log(CurrentScore);
    }
}
