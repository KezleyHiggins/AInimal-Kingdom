using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimalType { Elephant, Goat, Gorilla, Sloth, Wolf }

public class Animal : MonoBehaviour
{

    #region Variables

    [Header("Animal Stats")]
    public Region regionIAmIn;
    [HideInInspector] public TimeActive sleepBehaviour;
    [HideInInspector] public AnimalAwareness awareness;
    [HideInInspector] public AnimalMovement movement;
    [HideInInspector] public AnimalAnimationPlayer animalAnimationPlayer;
    private AnimalSoundPlayer animalSoundPlayer;
    public string animalName;
    public AnimalType animalType;
    public bool isDead = false;
    public int hunger = 20;
    public int hungerTickDownPerHour;
    public int hungerThreshold;
    public ResourceType desiredFood;
    [Space]
    [Header("Animal Behaviours")]
    public AnimalBehaviour[] behaviours;
    public AnimalBehaviour currentBehaviour;
    private AnimalBehaviour idleBehaviour;


    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        awareness = GetComponent<AnimalAwareness>();
        movement = GetComponent<AnimalMovement>();
        behaviours = GetComponents<AnimalBehaviour>();
        sleepBehaviour = GetComponent<TimeActive>();
        idleBehaviour = GetComponent<Idle>();
        animalSoundPlayer = GetComponent<AnimalSoundPlayer>();
        animalAnimationPlayer = GetComponent<AnimalAnimationPlayer>();
    }

    void Start()
    {
        DecideNextBehaviour();
    }

    #endregion

    #region Utility

    public void DecideNextBehaviour()
    {
        currentBehaviour = idleBehaviour;
        for (int i = 0; i < behaviours.Length; i++)
        {
            if (behaviours[i].IsThisBehaviourTriggered() == true)
            {
                if (behaviours[i] != currentBehaviour)
                {
                    currentBehaviour = behaviours[i];
                    movement.ResetMovementTarget();
                }
                break;
            }
        }

        animalSoundPlayer.DoNoiseCheck();
    }

    public bool DoesThisAnimalInteractWithThisResource(Resource resource)
    {
        if (resource.resourceType == desiredFood)
        {
            return true;
        }
        return false;
    }

    public void ConsumeResource(Resource resource)
    {
        hunger += resource.resourceValue;
        resource.gameObject.SetActive(false);
    }

    public void ReviveThisAnimal()
    {
        ResetAnimalStats();
        sleepBehaviour.FindSleepingSpot();
        isDead = false;
        gameObject.SetActive(true);
    }

    private void ResetAnimalStats()
    {
        hunger = 30;
    }

    public void TickDownHunger()
    {
        if (hunger < 0) { hunger = 0; }

        if (hunger != 0) { hunger -= hungerTickDownPerHour; }
    }

    #endregion

}
