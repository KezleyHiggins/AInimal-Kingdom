using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]

public class Hunger : AnimalBehaviour
{

    #region Variables

    Resource target;
    private ResourceType desiredResourceType;
    AreaManager areaManager;

    #endregion

    #region Awake, Start & Update

    private void Start()
    {
        areaManager = FindObjectOfType<AreaManager>();
        desiredResourceType = thisAnimal.desiredFood;
        //   StartCoroutine("HungerTicker");
    }

    #endregion

    #region Triggering The Behaviour

    public override bool IsThisBehaviourTriggered()
    {
        if (thisAnimal.hunger < thisAnimal.hungerThreshold)
        {
            return true;
        }

        return false;
    }

    #endregion

    #region Performing the Behaviour

    public override void DoBehaviour()
    {
        Resource nearbyResource = CheckIfThereIsFoodNearby();

        if (nearbyResource != null && thisAnimal.movement.hasMovementTarget == false)
        {
            thisAnimal.movement.MoveTowardsThis(nearbyResource);
        }
        else
        {
            LookForFood();
        }

    }

    Resource CheckIfThereIsFoodNearby()
    {

        target = thisAnimal.awareness.FindClosestResourceOfThisType(desiredResourceType);

        return target;
    }

    void LookForFood()
    {
        var newPosition = areaManager.GetRandomAreaWithThisTypeOfResource(thisAnimal.desiredFood).GetRandomRegionWithinThisArea().GetRandomPositionWithinThisRegion();
        thisAnimal.movement.MoveTowardsThis(newPosition);
        DisplaySadExpression();
    }

    private void DisplaySadExpression()
    {
        if (Random.Range(0f, 1f) > 0.6)
        {
            thisAnimal.animalAnimationPlayer.ShowSadExpression();
        }
    }

    #endregion

    #region Coroutines

    IEnumerator HungerTicker()
    {
        thisAnimal.hunger--;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("HungerTicker");
    }

    #endregion

    #region Utility

    public override string GetWhyAmIDoingThisText()
    {
        return "I am looking for food as my hunger is "+thisAnimal.hunger + " and the point at which I begin to look for something to eat is "+thisAnimal.hungerThreshold;
    }

    public override string GetCurrentlyDoingText()
    {
        return "I am looking for food to eat on the ground.";
    }

    public override string GetAIBehaviourName()
    {
        return "Hunger";
    }

    public override string GetDescriptionOfCurrentBehaviour()
    {
        return "The hunger behaviour is used to seek out food so as to eventually raise the hunger level above the minimum threshold for this animal.";
    }

    #endregion

}
