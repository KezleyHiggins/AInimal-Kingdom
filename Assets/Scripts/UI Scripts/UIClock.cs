using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIClock : MonoBehaviour
{

    #region Variables

    [SerializeField] TextMeshProUGUI timer, dayNightDisplay;
    TimeManager timeManager;

    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        UpdateTime();
        UpdateDayNightDisplay();
    }

    #endregion

    #region Tracking Time

    void UpdateTime()
    {
        if (timeManager.time < 10)
        {
            timer.text = "0"+timeManager.time.ToString()+":00";
        }
        else
        {
            timer.text = timeManager.time.ToString() + ":00";
        }
    }

    void UpdateDayNightDisplay()
    {
        dayNightDisplay.text = TimeManager.timeOfDay.ToString();
    }

    #endregion

}
