using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalObjectPooler : MonoBehaviour
{

    #region Variables

    public GameObject animalHolder;
    public AnimalPopulationBlueprint[] animalPopBlueprints;

    #endregion

    #region Filling the object pool

    void Awake()
    {
        FillObjectPool();
    }

    private void FillObjectPool()
    {
        for (int i = 0; i < animalPopBlueprints.Length; i++)
        {
            for (int j = 0; j < animalPopBlueprints[i].animalPopulationNum; j++)
            {
                Animal thisAnimal = Instantiate(animalPopBlueprints[i].prefab.gameObject, animalHolder.transform).GetComponent<Animal>();
                int randomNum = Random.Range(0, animalPopBlueprints[i].animalNames.Length);
                thisAnimal.animalName = animalPopBlueprints[i].animalNames[randomNum];
            }
        }
    }

    #endregion

    #region Classes

    [Serializable]
    public class AnimalPopulationBlueprint
    {
        public string name;
        public GameObject prefab;
        public int animalPopulationNum;
        public string[] animalNames;
    }

    #endregion

}
