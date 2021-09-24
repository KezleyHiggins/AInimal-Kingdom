using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum DayNight { Day, Night}

[RequireComponent(typeof(Animal))]

public class TimeActive : AnimalBehaviour
{

    #region Variables

    public DayNight currentTimeOfDay, sleepDuring;
    public Region sleepingSpot;
    [SerializeField] AreaManager areaManager;

    #endregion

    #region Awake, Start & Update

    private void Start()
    {
        areaManager = FindObjectOfType<AreaManager>();
    }

    #endregion

    #region Updating time and sleeping spot

    private void Update()
    {
        UpdateTimeOfDay();
        if (sleepingSpot == null) { FindSleepingSpot(); }
    }

    void UpdateTimeOfDay()
    {
        TimeOfDay currentTime = TimeManager.WhatTimeIsIt();
        if (currentTime == TimeOfDay.Morning || currentTime == TimeOfDay.Day)
        {
            currentTimeOfDay = DayNight.Day;
        }
        else
        {
            currentTimeOfDay = DayNight.Night;
        }
    }
    
    public void FindSleepingSpot()
    {
        Area _area = areaManager.GetRandomAreaWithThisTypeOfResource(thisAnimal.desiredFood);
        sleepingSpot = _area.GetRandomRegionWithinThisArea();
    }

    #endregion

    #region Triggering the Behaviour

        public override bool IsThisBehaviourTriggered()
    {
        if (currentTimeOfDay == sleepDuring)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Doing The Behaviour

    public override void DoBehaviour()
    {
        if (AmIInsideOfMySleepingRegion() == true)
        {
            Sleep();
        }
        else
        {
            MoveTowardsMySleepingRegion();
        }
    }

    private bool AmIInsideOfMySleepingRegion()
    {
        if (thisAnimal.regionIAmIn == sleepingSpot)
        {
            return true;
        }

        return false;
    }

    private void Sleep()
    {
        DisplayHappyExpression();
        thisAnimal.movement.ResetMovementTarget();
    }

    private void DisplayHappyExpression()
    {
        if (Random.Range(0f, 1f)>0.6)
        {
            thisAnimal.animalAnimationPlayer.ShowHappyExpression();
        }
    }

    private void MoveTowardsMySleepingRegion()
    {
        thisAnimal.movement.MoveTowardsThis(sleepingSpot);
    }

    #endregion

    #region Utility

    public override string GetWhyAmIDoingThisText()
    {
        return "I am currently attempting to sleep as it is " + currentTimeOfDay + " which is when I sleep.";
    }

    public override string GetCurrentlyDoingText()
    {
        return "I am currently attempting to sleep.";
    }

    public override string GetAIBehaviourName()
    {
        return "TimeActive";
    }

    public override string GetDescriptionOfCurrentBehaviour()
    {
        return "The TimeActive behaviour allows the animal to recognise the current time of day, and use that information to move to go to their sleeping spot if required.";
    }

    #endregion

}
