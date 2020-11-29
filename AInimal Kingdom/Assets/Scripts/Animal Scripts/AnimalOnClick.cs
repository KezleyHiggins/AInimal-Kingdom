using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]

public class AnimalOnClick : MonoBehaviour
{

    #region Variables

    UIController uiController;
    Animal thisAnimal;

    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        uiController = FindObjectOfType<UIController>();
        thisAnimal = GetComponent<Animal>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #endregion

    #region On Click

    public void OnMouseDown()
    {
        uiController.OpenAnimalViewer(thisAnimal);
    }

    #endregion

}
