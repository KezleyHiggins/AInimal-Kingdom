using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animal))]

public class Hunter : AnimalBehaviour
{

    #region Variables

    Animal target;
    ResourceManager resourceManager;

    #endregion

    #region Awake, Start & Update

    void Start()
    {
        resourceManager = FindObjectOfType<ResourceManager>();
    }

    void Update()
    {
        if (target != null && thisAnimal.currentBehaviour == this)
        {
            HuntPrey();
        }
    }

    #endregion

    #region Triggering The Behaviour

    public override bool IsThisBehaviourTriggered()
    {
        if (thisAnimal.hunger < thisAnimal.hungerThreshold && IsThereMeatNearby() == false)
        {
            return true;
        }

        return false;
    }

    private bool IsThereMeatNearby()
    {
        Resource nearbyMeat = thisAnimal.awareness.FindClosestResourceOfThisType(ResourceType.Meat);
        if (nearbyMeat != null)
        {
            return true;
        }
        return false;
    }

    #endregion

    #region Performing the Behaviour

    public override void DoBehaviour()
    {
        target = null;

        if (IsThereMeatNearby() == false && IsTherePreyNearby() == true)
        {
            HuntPrey();
        }
        else
        {
            LookForPrey();
        }
    }

    private bool IsTherePreyNearby()
    {
        var nearbyAnimals = thisAnimal.awareness.nearbyAnimals;
        if (nearbyAnimals.Count == 0) { return false; }

        for (int i = 0; i < nearbyAnimals.Count; i++)
        {
            if (nearbyAnimals[i].GetComponent<Prey>() != null)
            {
                target = nearbyAnimals[i];
                return true;
            }
        }

        return false;

    }

    private void HuntPrey()
    {
        if (target != null)
        {
            thisAnimal.movement.MoveTowardsThis(target);
        }
    }

    private void LookForPrey()
    {
        if (thisAnimal.awareness.nearbyAnimals.Count != 0)
        {
            target = thisAnimal.awareness.FindClosestPrey();
        }

        if (target == null)
        {
            SearchNearbyRegionForPrey();
        }
    }

    private void SearchNearbyRegionForPrey()
    {
        if (thisAnimal.regionIAmIn == null) { return; }
        Area area = thisAnimal.regionIAmIn.areaThisRegionIsIn;
        thisAnimal.movement.MoveTowardsThis(area.GetRandomRegionWithinThisArea());
    }

    #endregion

    #region Collision Handling

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Prey prey = collision.gameObject.GetComponent<Prey>();

        if (prey == null) { return; }

        resourceManager.SpawnResourcesAroundThisPoint(prey.transform.position, prey.meatSpawnAmount, thisAnimal.desiredFood);

        prey.KillThisAnimal();
    }

    #endregion

    #region Utility

    public override string GetWhyAmIDoingThisText()
    {
        if (target != null)
        {
            return "I am hunting "+target.animalName+"!";
        }
        return "I am hunting for food.";
    }

    public override string GetCurrentlyDoingText()
    {
        if (target != null)
        {
            return "I am hunting " + target.animalName+ ", a "+target.animalType+".";
        }
        else
        {
            return "I am looking for something to hunt.";
        }

    }

    public override string GetAIBehaviourName()
    {
        return "Hunter";
    }

    public override string GetDescriptionOfCurrentBehaviour()
    {
        return "The hunter behaviour allows the animal to seek out nearby animals with the 'Prey' behaviour in order to spawn meat in to consume.";
    }

    #endregion

}
