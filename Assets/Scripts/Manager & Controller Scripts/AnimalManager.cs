using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalManager : MonoBehaviour
{

    #region Variables

    [Header("References")]
    public List<Animal> allAnimals;
    [SerializeField] private GameObject animalHolder;
    private AreaManager areaManager;

    [Header("Population")]
    public AnimalPopulation[] animalPopulations;

    #endregion
    void Awake()
    {
        areaManager = FindObjectOfType<AreaManager>();
    }

    void Start()
    {
        FindAllAnimals();
        FillAnimalList();
        SetAllAnimalsToDead();
    }

    #region Managing Animal Populations

    public void ManageAnimalPopulations()
    {
        CheckEachAnimalPopulation();
    }

    void SetAllAnimalsToDead()
    {
        for (int i = 0; i < allAnimals.Count; i++)
        {
            allAnimals[i].isDead = true;
            allAnimals[i].gameObject.SetActive(false);
        }
    }
    
    void FindAllAnimals()
    {
        var _allAnimals = animalHolder.GetComponentsInChildren<Animal>();
        allAnimals.Clear();
        for (int i = 0; i < _allAnimals.Length; i++)
        {
            allAnimals.Add(_allAnimals[i]);
        }
    }

    void CheckEachAnimalPopulation()
    {
        for (int i = 0; i < animalPopulations.Length; i++)
        {
            CheckThisAnimalPopulation(animalPopulations[i]);
        }
    }

    void CheckThisAnimalPopulation(AnimalPopulation animalPopulation)
    {
        if (DoesThisAnimalPopulationNeedToBeRepopulated(animalPopulation) == true)
        {
            RepopulateThisAnimalPopulation(animalPopulation);
        }
    }

    bool DoesThisAnimalPopulationNeedToBeRepopulated(AnimalPopulation animalPopulation)
    {
        if (animalPopulation.maxNumberThatCanBeDead < AmountOfDeadAnimalsInThisPopulation(animalPopulation))
        {
            return true;
        }
        return false;
    }

    int AmountOfDeadAnimalsInThisPopulation(AnimalPopulation animalPopulation)
    {
        int deadAnimalCount = 0;

        for (int i = 0; i < animalPopulation.animals.Count; i++)
        {
            if (animalPopulation.animals[i].isDead == true)
            {
                deadAnimalCount++;
            }
        }

        return deadAnimalCount;
    }

    void RepopulateThisAnimalPopulation(AnimalPopulation animalPopulation)
    {
        int animalsBeingRevived = Random.Range(1, AmountOfDeadAnimalsInThisPopulation(animalPopulation));
        for (int i = 0; i < animalsBeingRevived; i++)
        {
            for (int j = 0; j < animalPopulation.animals.Count; j++)
            {
                if (animalPopulation.animals[i].isDead == true)
                {
                    ReviveThisAnimal(animalPopulation.animals[i], animalPopulation);
                }
            }
        }
    }

    void ReviveThisAnimal(Animal animal, AnimalPopulation animalPopulation)
    {
        animal.ReviveThisAnimal();
        PlaceAnimalAtSpawnLocation(animal, animalPopulation);
    }

    void PlaceAnimalAtSpawnLocation(Animal animal, AnimalPopulation animalPopulation)
    {
        for (int i = 0; i < (areaManager.allAreas.Count * 4); i++)
        {
            int randomNum = Random.Range(0, areaManager.allAreas.Count);

            if (areaManager.allAreas[randomNum].CanThisTypeOfAnimalSpawnHere(animal.animalType) == true)
            {
                animal.transform.position = areaManager.allAreas[randomNum].GetRandomRegionWithinThisArea().GetRandomPositionWithinThisRegion();
                break;
            }
        }
    }

    #endregion

    #region Utility
    public void UpdateAnimalBehaviours()
    {
        for (int i = 0; i < allAnimals.Count; i++)
        {
            allAnimals[i].DecideNextBehaviour();
        }
    }

    public void UpdateAnimalHungers()
    {
        for (int i = 0; i < allAnimals.Count; i++)
        {
            allAnimals[i].TickDownHunger();
        }
    }

    public void DoAnimalBehaviours()
    {
        for (int i = 0; i < allAnimals.Count; i++)
        {
            if (allAnimals[i].currentBehaviour != null)
            {
                allAnimals[i].currentBehaviour.DoBehaviour();
            }
        }
    }
    #endregion

    #region Populating Animal List

    void FillAnimalList()
    {
        for (int i = 0; i < animalPopulations.Length; i++)
        {
            for (int j = 0; j < allAnimals.Count; j++)
            {
                if (allAnimals[j].animalType == animalPopulations[i].animalType)
                {
                    animalPopulations[i].animals.Add(allAnimals[j]);
                }
            }
        }
    }

    #endregion

    #region Classes

    [Serializable]
    public class AnimalPopulation
    {
        public AnimalType animalType;
        public int maxNumberThatCanBeDead;
        public List<Animal> animals;
    }

    #endregion

}