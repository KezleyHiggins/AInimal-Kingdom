using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObjectPooler : MonoBehaviour
{

    #region Variables

    public GameObject resourceHolder;
    public ResourcePoolBlueprint[] resourceBlueprints;

    #endregion

    #region Awake & Filling the object pool

    private void Awake()
    {
        FillObjectPool();
    }

    void FillObjectPool()
    {
        for (int i = 0; i < resourceBlueprints.Length; i++)
        {
            for (int j = 0; j < resourceBlueprints[i].poolSize; j++)
            {
                Instantiate(resourceBlueprints[i].prefab, resourceHolder.transform);
            }
        }
    }

    #endregion

    #region Classes

    [System.Serializable]
    public class ResourcePoolBlueprint
    {
        public string resourceName;
        public int poolSize;
        public GameObject prefab;
    }

    #endregion

}
