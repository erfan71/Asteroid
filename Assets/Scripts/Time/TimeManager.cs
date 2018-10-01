using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    
    private System.Diagnostics.Stopwatch stopwatch=new System.Diagnostics.Stopwatch();
    public TimeHudManager timeHUD;
    Coroutine updatingRoutine;
    private bool isTimeTick;
    public void StartTimer()
    {
        stopwatch = new System.Diagnostics.Stopwatch();
        stopwatch.Start();
        isTimeTick = true;
        if (updatingRoutine != null)
            StopCoroutine(updatingRoutine);
        updatingRoutine= StartCoroutine(UpdatingRoutine());
    }

   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseTimer();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ContinueTimer();
        }
    }

   
    public void ContinueTimer()
    {
        stopwatch.Start();
        isTimeTick = true;
    }
    public void PauseTimer()
    {
        stopwatch.Stop();
        isTimeTick = false;
    }
    public float GetElapsedtimeInSeconds()
    {
        return (float)stopwatch.Elapsed.TotalSeconds;
    }
    private IEnumerator UpdatingRoutine()
    {
        while (true)
        {
            if (isTimeTick)
                timeHUD.UpdateTime(GetElapsedtimeInSeconds());
            yield return new WaitForSeconds(1);
        }
    }

}
