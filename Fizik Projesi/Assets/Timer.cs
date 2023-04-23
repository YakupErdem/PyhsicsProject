using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float passedTime;

    private bool _isTimerOn = false;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }

    public void StartTimer()
    {
        _isTimerOn = true;
    }

    public void EndTimer()
    {
        _isTimerOn = false;
    }

    public float GetCurrentTime()
    {
        int indexOfDot = 0;
        float newTime = 0;
        foreach (var letter in passedTime.ToString().ToCharArray())
        {
            if (letter == '.') 
            {
                break;
            }

            indexOfDot++;
        }
        
        Debug.Log("Passed Time: "+ passedTime.ToString());
        if (passedTime.ToString().Length > 3)newTime = float.Parse(passedTime.ToString().Substring(0, indexOfDot + 3));
        try
        {
            
        }
        catch (Exception e)
        {
            throw;
        }
        
        return newTime;
    }

    public void RefreshTimer()
    {
        passedTime = 0;
    }

    private void FixedUpdate()
    {
        if (_isTimerOn)passedTime += Time.deltaTime;
    }
}
