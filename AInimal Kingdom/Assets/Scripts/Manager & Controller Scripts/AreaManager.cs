using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AreaManager : MonoBehaviour
{

    #region Variables

    public List<Area> allAreas;
    private SpriteRenderer mapBounds;

    #endregion

    #region Awake

    void Awake()
    {
        mapBounds = GetComponentInChildren<SpriteRenderer>();
        FindAllAreas();
        FillOutAreas(); 
    }

    #endregion

    #region Instantiating Lists

    void FindAllAreas()
    {
        allAreas.Clear();
        var _allAreas = FindObjectsOfType<Area>();
        for (int i = 0; i < _allAreas.Length; i++)
        {
            allAreas.Add(_allAreas[i]);
        }
    }

    void FillOutAreas()
    {
        for (int i = 0; i < allAreas.Count; i++)
        {
            allAreas[i].InstantiateArea();
        }
    }

    #endregion

    #region Utility

    public void PlaceThisResourceWithinAnApplicableArea(Resource resource)
    {
        List<Area> applicableAreas = FindAllAreasThatCanHaveThisResource(resource);
        int randomNum = Random.Range(0, applicableAreas.Count);
        Region regionBeingUsed = applicableAreas[randomNum].GetRandomRegionWithinThisArea();
        resource.transform.position = regionBeingUsed.GetRandomPositionWithinThisRegion();
    }

    List<Area> FindAllAreasThatCanHaveThisResource(Resource resource)
    {
        List<Area> areasThatCanStoreThisResource = new List<Area>();
        for (int i = 0; i < allAreas.Count; i++)
        {
            if (allAreas[i].IsThisResourceTypeBeStoredWithin(resource.resourceType))
            {
                areasThatCanStoreThisResource.Add(allAreas[i]);
            }
        }

        return areasThatCanStoreThisResource;
    }

    public Area GetRandomAreaWithThisTypeOfResource(ResourceType resourceType)
    {
        List<Area> areasWithThisResource = new List<Area>();

        for (int i = 0; i < allAreas.Count; i++)
        {
            if (allAreas[i].IsThisResourceTypeBeStoredWithin(resourceType))
            {
                areasWithThisResource.Add(allAreas[i]);
            }
        }

        return areasWithThisResource[Random.Range(0, areasWithThisResource.Count)];
    }

    public bool IsTheAreaListPopulated()
    {
        if (allAreas.Count < 1)
        {
            return false;
        }
        return true;
    }

    public bool IsThisPointWithinTheBoundsOfTheMap(Vector3 point)
    {
        return mapBounds.bounds.Contains(point);
    }

    #endregion

}
