using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    #region Variables

    public string areaName;
    public List<ResourceType> resourceTypesHeldWithin;
    public List<AnimalType> typesOfAnimalsThatCanSpawnHere;
    [HideInInspector] public List<Region> regions;

    #endregion

    #region Awake, Start & Update

    private void Awake()
    {
        gameObject.name = areaName;
        InstantiateArea();
    }

    #endregion

    #region Utility

    public void InstantiateArea()
    {
        var _regions = GetComponentsInChildren<Region>();
        regions.Clear();
        for (int i = 0; i < _regions.Length; i++)
        {
            _regions[i].regionName = areaName + i;
            _regions[i].gameObject.name = areaName + i;
            regions.Add(_regions[i]);
        }
    }

    public Region GetRandomRegionWithinThisArea()
    {
        int randomNum = Random.Range(0, regions.Count);
        return regions[randomNum];
    }

    public bool IsThisResourceTypeBeStoredWithin(ResourceType resourceType)
    {
        for (int i = 0; i < resourceTypesHeldWithin.Count; i++)
        {
            if (resourceTypesHeldWithin[i] == resourceType)
            {
                return true;
            }
        }

        return false;
    }

    public bool CanThisTypeOfAnimalSpawnHere(AnimalType animalType)
    {
        return typesOfAnimalsThatCanSpawnHere.Contains(animalType);
    }

    #endregion

}
