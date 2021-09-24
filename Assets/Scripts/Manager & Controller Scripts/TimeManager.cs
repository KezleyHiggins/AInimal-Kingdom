using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimeOfDay { Morning, Day, Afternoon, Night}
public enum TimeScale { Slow, Normal, Fast}

public class TimeManager : MonoBehaviour
{

    #region Variables

    [Header("Current Time Info")]
    public int time; 
    static public TimeOfDay timeOfDay = TimeOfDay.Morning;
    public TimeScale timeScale = TimeScale.Normal;
    [Space]
    [Header("Time Modifier")]
    [SerializeField] float secondsPerHour = 1f;
    float timeScaleModifier = 1f;
    private GameManager gameManager;

    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        SetTimeScaleModifier();
        SetTimeOfDay();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #endregion

    #region Core Functionality

    public void StartTimeCoroutine()
    {
        StartCoroutine("AdvanceTime");
    }

    public void StopTimeCoroutine()
    {
        StopCoroutine("AdvanceTime");
    }

    IEnumerator AdvanceTime()
    {
        SetTimeScaleModifier();

        yield return new WaitForSeconds(secondsPerHour * timeScaleModifier);

        AdvanceAnHour();

        if (time > 24)
        {
            SetNewDay();
        }

        SetTimeOfDay();

        gameManager.OnHourAdvance();

        StartCoroutine("AdvanceTime");
    }

    void AdvanceAnHour()
    {
        time += 1;
    }

    void SetNewDay()
    {
        time = 0;
    }

    void SetTimeOfDay()
    {
        if (time <= 5 || time >= 20)
        {
            timeOfDay = TimeOfDay.Night;
        }
        else if (time <= 8)
        {
            timeOfDay = TimeOfDay.Morning;
        }
        else if (time <= 12)
        {
            timeOfDay = TimeOfDay.Day;
        }
        else if (time >= 17)
        {
            timeOfDay = TimeOfDay.Afternoon;
        }
    }

    #endregion

    #region Changing Time Scale

    public void SetTimeScaleToSlow()
    {
        timeScale = TimeScale.Slow;
    }

    public void SetTimeScaleToNormal()
    {
        timeScale = TimeScale.Normal;
    }

    public void SetTimeScaleToFast()
    {
        timeScale = TimeScale.Fast;
    }

    public void RaiseTimeScale()
    {
        if (CanTimeScaleBeRaised() == true)
        {
            if (timeScale == TimeScale.Slow)
            {
                timeScale = TimeScale.Normal;
            }
            else
            {
                timeScale = TimeScale.Fast;
            }
        }

        SetTimeScaleModifier();
    }

    public bool CanTimeScaleBeRaised()
    {
        if (timeScale != TimeScale.Fast)
        {
            return true;
        }
        return false;
    }

    public void LowerTimeScale()
    {
        if (CanTimeScaleBeLowered() == true)
        {
            if (timeScale == TimeScale.Fast)
            {
                timeScale = TimeScale.Normal;
            }
            else
            {
                timeScale = TimeScale.Slow;
            }
        }

        SetTimeScaleModifier();
    }

    public bool CanTimeScaleBeLowered()
    {
        if (timeScale != TimeScale.Slow)
        {
            return true;
        }
        return false;
    }

    void SetTimeScaleModifier()
    {
        if (timeScale == TimeScale.Fast)
        {
            timeScaleModifier = 0.5f;
        }
        else if (timeScale == TimeScale.Normal)
        {
            timeScaleModifier = 1f;
        }
        else if (timeScale == TimeScale.Slow)
        {
            timeScaleModifier = 2f;
        }
    }

    #endregion

    #region Querying Information

    public static TimeOfDay WhatTimeIsIt()
    {
        return timeOfDay;
    }

    #endregion

}
