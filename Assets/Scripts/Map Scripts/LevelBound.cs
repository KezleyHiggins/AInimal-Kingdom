using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBound : MonoBehaviour
{

    #region Variables

    List<Animal> allAnimals;
    [SerializeField] SpriteRenderer levelBound;

    #endregion

    #region Awake, Start & Update

    void Start()
    {
        AnimalManager animalManager = FindObjectOfType<AnimalManager>();
        if (animalManager == null) { return; }
        allAnimals = animalManager.allAnimals;
    }

    void Update()
    {
        MakePreyCowerIfRequired();
    }

    #endregion

    #region Animal Interaction

    private void MakePreyCowerIfRequired()
    {
        if (allAnimals == null) { return; }

        for (int i = 0; i < allAnimals.Count; i++)
        {
            Vector3 thisAnimalPos = allAnimals[i].transform.position;

            if (levelBound.bounds.Contains(thisAnimalPos))
            {
                Prey thisPrey = allAnimals[i].GetComponent<Prey>();
                if (thisPrey != null)
                {
                    thisPrey.isCowering = true;
                }
            }
            else
            {
                Prey thisPrey = allAnimals[i].GetComponent<Prey>();
                if (thisPrey != null)
                {
                    thisPrey.isCowering = false;
                }
            }
        }
    }

    #endregion

}
