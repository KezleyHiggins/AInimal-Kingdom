using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TimeManager timeManager;
    ResourceManager resourceManager;
    AnimalManager animalManager;

    void Awake()
    {
        timeManager = GetComponent<TimeManager>();
        resourceManager = GetComponent<ResourceManager>();
        animalManager = GetComponent<AnimalManager>();
    }

    void Start()
    {
        timeManager.StartTimeCoroutine();
    }

    public void OnHourAdvance()
    {
        resourceManager.DoResourceActions();
        animalManager.ManageAnimalPopulations();
        animalManager.UpdateAnimalBehaviours();
        animalManager.UpdateAnimalHungers();
        animalManager.DoAnimalBehaviours();
    }

    public void PauseGame()
    {
        timeManager.StopTimeCoroutine();
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        timeManager.StartTimeCoroutine();
        Time.timeScale = 1;
    }

}
