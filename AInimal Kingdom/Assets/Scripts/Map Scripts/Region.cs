using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{

    #region Variables

    public string regionName;
    public Area areaThisRegionIsIn;
    public Transform centerOfRegion;
    [SerializeField] Transform topLeft, bottomRight;
    [HideInInspector] public SpriteRenderer regionBounds;
    List<Animal> allAnimals;


    #endregion

    #region Awake, Start & Update

    private void Awake()
    {
        AnimalManager animalManager = FindObjectOfType<AnimalManager>();
        allAnimals = animalManager.allAnimals;
        areaThisRegionIsIn = GetComponentInParent<Area>();
        regionBounds = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FindAnimalsInThisRegion();
    }

    #endregion

    #region Utility

    void FindAnimalsInThisRegion()
    {
        for (int i = 0; i < allAnimals.Count; i++)
        {
            if (regionBounds.bounds.Contains(allAnimals[i].transform.position))
            {
                allAnimals[i].regionIAmIn = this;
            }
        }
    }

    public Vector3 GetRandomPositionWithinThisRegion()
    {
        float x = Random.Range(topLeft.position.x, bottomRight.position.x);
        float y = Random.Range(topLeft.position.y, bottomRight.position.y);

        float perlinValue = Mathf.PerlinNoise(x, y);

        if (perlinValue > 0.7f)
        {
            x += perlinValue;
        }
        else if (perlinValue > 0.4f)
        {
            y += perlinValue;
        }
        else if (perlinValue > 0f)
        {
            x += perlinValue;
            y += perlinValue;
        }

        return new Vector3(x, y, 0);
    }

    #endregion

}
