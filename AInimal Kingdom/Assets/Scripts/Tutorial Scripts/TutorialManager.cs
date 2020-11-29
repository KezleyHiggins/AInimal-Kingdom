using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    #region Variables

    TutorialSegment[] tutorialSegments;

    #endregion

    #region Methods

    private void Awake()
    {
        tutorialSegments = FindObjectsOfType<TutorialSegment>();
    }

    public void TurnOffAllUI()
    {
        for (int i = 0; i < tutorialSegments.Length; i++)
        {
            tutorialSegments[i].TurnOffUI();
        }
    }

    #endregion

}
