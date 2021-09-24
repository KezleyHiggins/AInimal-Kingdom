using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeScale : MonoBehaviour
{

    #region Variables

    TimeManager timeManager;
    [SerializeField] Button slowButton, normalButton, fastButton;

    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Update()
    {
        ManageButtons();
    }

    #endregion

    #region Managing Buttons
    void ManageButtons()
    {
        if (timeManager.timeScale == TimeScale.Slow)
        {
            slowButton.interactable = false;
            normalButton.interactable = true;
            fastButton.interactable = true;
        }
        else if (timeManager.timeScale == TimeScale.Normal)
        {
            slowButton.interactable = true;
            normalButton.interactable = false;
            fastButton.interactable = true;
        }
        else if (timeManager.timeScale == TimeScale.Fast)
        {
            slowButton.interactable = true;
            normalButton.interactable = true;
            fastButton.interactable = false;
        }
    }
    #endregion

}
