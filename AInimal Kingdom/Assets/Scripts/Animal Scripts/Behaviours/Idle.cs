using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animal))]

public class Idle : AnimalBehaviour
{

    #region Variables

    private AreaManager areaManager;
    private bool willWander = true;
    private int wanderCount = 0;

    #endregion

    #region Awake, Start & Update

    private void Awake()
    {
        thisAnimal = GetComponent<Animal>();
        areaManager = FindObjectOfType<AreaManager>();
    }

    #endregion

    #region Triggering The Behaviour

    public override bool IsThisBehaviourTriggered()
    {
        return true;
    }

    #endregion

    #region Performing the Behaviour
    public override void DoBehaviour()
    {
        Wander();
        DisplayHappyExpression();
    }

    private void Wander()
    {
        if (willWander == true)
        {
            thisAnimal.movement.MoveToRandomLocationInThisArea(areaManager.GetRandomAreaWithThisTypeOfResource(thisAnimal.desiredFood));
            wanderCount = 1;
            willWander = false;
            StartCoroutine("WanderCountdown");
        }
    }

    private void DisplayHappyExpression()
    {
        if (Random.Range(0f, 1f) > 0.6)
        {
            thisAnimal.animalAnimationPlayer.ShowHappyExpression();
        }
    }

    private IEnumerator WanderCountdown()
    {
        wanderCount--;
        yield return new WaitForSeconds(1f);
        if (wanderCount != 0) { StartCoroutine("WanderCountdown"); }
        else { willWander = true; }
    }
    #endregion

    #region Utility

    public override string GetWhyAmIDoingThisText()
    {
        return "I am wandering around no other behaviour has been triggered.";
    }

    public override string GetCurrentlyDoingText()
    {
        return "I am wandering around happily.";
    }

    public override string GetAIBehaviourName()
    {
        return "Idle";
    }

    public override string GetDescriptionOfCurrentBehaviour()
    {
        return "The idle behaviour serves as the default behaviour that the animal performs when no other behaviour is triggered.";
    }

    #endregion

}
