using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class AnimalAwareness : MonoBehaviour
{

    #region Variables 

    Animal thisAnimal;
    private List<Animal> allAnimals;
    private List<Resource> allResources;
    [Header("Awareness")]
    public List<Animal> nearbyAnimals;
    public List<Resource> nearbyResources;

    [SerializeField] private float visionDistance;

    #endregion

    #region Awake, Start & Update

    void Start()
    {
        thisAnimal = GetComponent<Animal>();
        InitializeAnimal();
    }

    void Update()
    {
        FindNearbyAnimals();
        FindNearbyResources();
    }

    void InitializeAnimal()
    {
        var resourceManager = FindObjectOfType<ResourceManager>();
        allResources = resourceManager.allResources;
        var animalManager = FindObjectOfType<AnimalManager>();
        allAnimals = animalManager.allAnimals;
    }

    #endregion

    #region Finding nearby animals and resources

    public void FindWhatIsNearMe()
    {
        FindNearbyAnimals();
        FindNearbyResources();
    }

    void FindNearbyResources()
    {
        nearbyResources.Clear();
        for (int i = 0; i < allResources.Count; i++)
        {
            if (allResources[i].gameObject.activeSelf == true && IsThisObjectWithinMyAwarenessRange(allResources[i].transform) == true)
            {
                if (allResources[i].resourceType == thisAnimal.desiredFood)
                {
                    nearbyResources.Add(allResources[i]);
                }
            }
        }
    }

    void FindNearbyAnimals()
    {
        nearbyAnimals.Clear();
        for (int i = 0; i < allAnimals.Count; i++)
        {
            if (allAnimals[i].gameObject.activeSelf == true && IsThisObjectWithinMyAwarenessRange(allAnimals[i].transform) == true)
            {
                if (allAnimals[i].gameObject != gameObject)
                {
                    nearbyAnimals.Add(allAnimals[i]);
                }
            }
        }
    }

    #endregion

    #region Distance Checking

    bool IsThisObjectWithinMyAwarenessRange(Transform targetPos)
    {
        Vector3 delta = targetPos.position - transform.position;
        float sqrDistance = delta.sqrMagnitude;
        if (sqrDistance < visionDistance)
        {
            return true;
        }
        return false;
    }

    public Animal FindClosestPrey()
    {
        List<Animal> allPrey = new List<Animal>();

        for (int i = 0; i < nearbyAnimals.Count; i++)
        {
            if (nearbyAnimals[i].GetComponent<Prey>() != null)
            {
                allPrey.Add(nearbyAnimals[i]);
            }
        }

        if (allPrey.Count == 0) { return null; }

        if (allPrey.Count == 1) { return allPrey[0]; }

        Animal closestPrey = FindClosestAnimalInThisList(allPrey);

        return closestPrey;

    }

    private Animal FindClosestAnimalInThisList(List<Animal> allPrey)
    {
        float closestDistance = 1000;
        Animal prey = allPrey[0];

        for (int i = 0; i < allPrey.Count; i++)
        {
            if (Vector3.Distance(prey.transform.position, allPrey[i].transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(prey.transform.position, allPrey[i].transform.position);
                prey = allPrey[i];
            }
        }

        return prey;
    }

    public Resource FindClosestResourceOfThisType(ResourceType resourceType)
    {
        List<Resource> theseResources = FindAllResourcesOfThisType(resourceType);
        if (theseResources.Count == 0) { return null; }
        else if(theseResources.Count == 1) { return theseResources[0]; }
        float closestDistance = 100;
        Resource lastClosestResource = theseResources[0];
        for (int i = 0; i < theseResources.Count; i++)
        {
            if (Vector3.Distance(theseResources[i].transform.position, lastClosestResource.transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(theseResources[i].transform.position, lastClosestResource.transform.position);
                lastClosestResource = theseResources[i];
            }
        }

        return lastClosestResource;
    }

    private List<Resource> FindAllResourcesOfThisType(ResourceType resourceType)
    {
        List<Resource> resourcesOfThisType = new List<Resource>();
        for (int i = 0; i < nearbyResources.Count; i++)
        {
            if (nearbyResources[i].resourceType == resourceType)
            {
                resourcesOfThisType.Add(nearbyResources[i]);
            }
        }
        return resourcesOfThisType;
    }

    #endregion

}
