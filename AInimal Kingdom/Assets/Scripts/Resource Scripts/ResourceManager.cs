using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ResourceManager : MonoBehaviour
{

    #region Variables

    [HideInInspector] public AreaManager areaManager;
    public List<Resource> allResources;
    [SerializeField] private GameObject resourceHolder;
    public int minResourceThreshold;
    public List<Resource> grassPool, fruitPool, meatPool, resourcePool;

    #endregion

    #region Awake & Start

    private void Awake()
    {
        areaManager = FindObjectOfType<AreaManager>();
    }

    void Start()
    {
        FillPools();
    }

    #endregion

    #region Managing Resources

    public void DoResourceActions()
    {
        DoResourceCheck(grassPool);
        DoResourceCheck(fruitPool);
    }

    void DoResourceCheck(List<Resource> resources)
    {
        int activeResourcesAmount = 0;
        for (int i = 0; i < resources.Count; i++)
        {
            if (resources[i].gameObject.activeSelf == true)
            {
                activeResourcesAmount += 1;
            }
        }

        if (activeResourcesAmount <= resources.Count - minResourceThreshold)
        {
            RepopulateResource(resources, activeResourcesAmount);
        }

    }

    private void RepopulateResource(List<Resource> resources, int activeObjs)
    {
        int amountOfResourcesThatArentActive = resources.Count - activeObjs;
        int amountOfResourcesToBeReactivated = Random.Range(1, amountOfResourcesThatArentActive);
        for (int i = 0; i < amountOfResourcesToBeReactivated; i++)
        {
            for (int j = 0; j < resources.Count; j++)
            {
                if (resources[j].gameObject.activeSelf == false)
                {
                    PlaceResource(resources[j]);
                    resources[j].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }

    public void SpawnResourcesAroundThisPoint(Vector3 target, int amount, ResourceType resourceType)
    {
        List<Resource> resourcesToBeUsed = FindAllResourcesOfThisType(resourceType);
        int currentCount = 0;
        for (int i = 0; i < resourcesToBeUsed.Count; i++)
        {
            if (resourcesToBeUsed[i].gameObject.activeSelf == false && resourcesToBeUsed[i].isConsumed == true)
            {
                currentCount++;
                SpawnThisResourceAroundThisPoint(resourcesToBeUsed[i], target);
                resourcesToBeUsed[i].gameObject.SetActive(true);
                resourcesToBeUsed[i].isConsumed = false;
            }

            if (currentCount >= amount) { break; }

        }

    }

    private void SpawnThisResourceAroundThisPoint(Resource resource, Vector3 target)
    {
        float x = target.x;
        float y = target.y;
        x += Random.Range(-5, 5);
        y += Random.Range(-5, 5);
        for (int i = 0; i < 30; i++)
        {
            if (areaManager.IsThisPointWithinTheBoundsOfTheMap(new Vector2(x, y)) == true)
            {
                resource.transform.position = new Vector2(x, y);
                break;
            }
        }
    }

    private List<Resource> FindAllResourcesOfThisType(ResourceType resourceType)
    {
        List<Resource> listToBeReturned = new List<Resource>();

        for (int i = 0; i < allResources.Count; i++)
        {
            if (allResources[i].resourceType == resourceType)
            {
                listToBeReturned.Add(allResources[i]);
            }
        }

        return listToBeReturned;
    }

    private void PlaceResource(Resource resource)
    {
        areaManager.PlaceThisResourceWithinAnApplicableArea(resource);
    }

    #endregion

    #region Filling Object Pools

    private void FindAllResources()
    {
        var _allResources = resourceHolder.GetComponentsInChildren<Resource>();
        allResources.Clear();
        for (int i = 0; i < _allResources.Length; i++)
        {
            allResources.Add(_allResources[i]);
            _allResources[i].isConsumed = true;
        }
    }

    private void FillPools()
    {
        Resource[] allResources = FindObjectsOfType<Resource>();
        FillResourcePool(allResources);
        FillGrassPool(allResources);
        FillFruitPool(allResources);
        FillMeatPool(allResources);
        HideAllPooledObjects();
    }

    void FillResourcePool(Resource[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            resourcePool.Add(resources[i]);
        }
    }

    void FillGrassPool(Resource[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].resourceType == ResourceType.Grass)
            {
                grassPool.Add(resources[i]);
            }
        }
    }

    void FillFruitPool(Resource[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].resourceType == ResourceType.Fruit)
            {
                fruitPool.Add(resources[i]);
            }
        }
    }

    void FillMeatPool(Resource[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            if (resources[i].resourceType == ResourceType.Meat)
            {
                meatPool.Add(resources[i]);
            }
        }
    }

    void HideAllPooledObjects()
    {
        FindAllResources();
        for (int i = 0; i < grassPool.Count; i++)
        {
            grassPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < fruitPool.Count; i++)
        {
            fruitPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < meatPool.Count; i++)
        {
            meatPool[i].gameObject.SetActive(false);
        }
    }

    #endregion

}
