using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHUDManager : MonoBehaviour {
    public Text textVar;
    public void UpdateScore(float currentScore)
    {
        textVar.text = currentScore.ToString();
    }
}
