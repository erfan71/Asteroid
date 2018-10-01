using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeHudManager : MonoBehaviour {

    public Text textVar;

    public void UpdateTime(float currentTime)
    {
        string correctformat = ConvertSecondToClockFormat((int)currentTime);
        textVar.text = correctformat;
    }
    public string ConvertSecondToClockFormat(int second)
    {
        int min = second / 60;
        int sec = second % 60;

        string final = min.ToString("00") +" : "+ sec.ToString("00");
        return final;
    }
}
