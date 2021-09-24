using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(Animal))]

public class Prey : AnimalBehaviour
{

    #region Variables    

    [SerializeField] Animal predator;
    public int meatSpawnAmount;
    public bool isCowering = false;

    #endregion

    #region Awake, Start & Update

    #endregion

    #region Triggering The Behaviour

    public override bool IsThisBehaviourTriggered()
    {
        if (CanISeeAPredator() == true)
        {
            UpdateSleepingSpot();
            return true;
        }
        return false;
    }

    bool CanISeeAPredator()
    {
        predator = FindNearbyPredator();
        if (predator == null)
        {
            return false;
        }
        return true;
    }

    private Animal FindNearbyPredator()
    {
        List<Animal> nearbyAnimals = thisAnimal.awareness.nearbyAnimals;
        for (int i = 0; i < nearbyAnimals.Count; i++)
        {
            if (nearbyAnimals[i].GetComponent<Hunter>() != null)
            {
                return nearbyAnimals[i];
            }
        }
        return null;
    }
    #endregion

    #region Performing the Behaviour

    public override void DoBehaviour()
    {
        if (isCowering == true) { Hide(); }
        else { RunAwayFromPredator(); }
    }

    void RunAwayFromPredator()
    {
        if (predator.regionIAmIn == thisAnimal.sleepBehaviour.sleepingSpot)
        {
            thisAnimal.sleepBehaviour.FindSleepingSpot();
        }
        Vector3 runningAwayDirection = FindPositionFurthestAwayFromPredator();
        thisAnimal.movement.MoveTowardsThis(runningAwayDirection);
        DisplaySadExpression();
    }

    private void DisplaySadExpression()
    {
        if (Random.Range(0f, 1f) > 0.6)
        {
            thisAnimal.animalAnimationPlayer.ShowSadExpression();
        }
    }

    private Vector3 FindPositionFurthestAwayFromPredator()
    {
        List<Vector3> directions = GetDirectionList();
        Vector3 furthestDirection = Vector3.zero;
        float furthestDistance = 0;
        for (int i = 0; i < directions.Count; i++)
        {
            if (Vector3.Distance(predator.transform.position, transform.position + directions[i]) > furthestDistance)
            {
                furthestDistance = Vector3.Distance(predator.transform.position, transform.position + directions[i]);
                furthestDirection = directions[i];
            }
        }
        Vector3 runAwayPosition = transform.position + furthestDirection;
        return runAwayPosition;
    }

    private List<Vector3> GetDirectionList()
    {
        Vector3 north = new Vector3(0, 10);
        Vector3 south = new Vector3(0, -10);
        Vector3 west = new Vector3(-10, 0);
        Vector3 east = new Vector3(10, 0);
        Vector3 northEast = north + east;
        Vector3 northWest = north + west;
        Vector3 southEast = south + east;
        Vector3 southWest = south + west;
        List<Vector3> directions = new List<Vector3>();
        directions.Add(north);
        directions.Add(south);
        directions.Add(west);
        directions.Add(east);
        directions.Add(northEast);
        directions.Add(northWest);
        directions.Add(southEast);
        directions.Add(southWest);
        return directions;
    }

    private void Hide()
    {
        // Play hiding animations
        thisAnimal.movement.ResetMovementTarget();
    }

    #endregion

    #region Utility

    private void UpdateSleepingSpot()
    {
        if (thisAnimal.sleepBehaviour.sleepingSpot == predator.regionIAmIn)
        {
            thisAnimal.sleepBehaviour.sleepingSpot = null;
        }
    }


    public void KillThisAnimal()
    {
        thisAnimal.isDead = true;
        gameObject.SetActive(false);
    }

    public override string GetWhyAmIDoingThisText()
    {
        if (predator != null)
        {
            return "I can see " + predator.animalName +", a "+predator.animalType+ ", who has the predator component. Therefore I am running away.";
        }

        return "I am running away because I saw a predator.";
    }

    public override string GetCurrentlyDoingText()
    {
        return "I am currently running away from a predator.";
    }

    public override string GetAIBehaviourName()
    {
        return "Prey";
    }

    public override string GetDescriptionOfCurrentBehaviour()
    {
        return "The Prey behaviour allows an animal to recognise other animals that have the 'hunter' component, and allows them to flee away from the animal.";
    }

    #endregion

}
