using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ResourceCollision : MonoBehaviour
{

    #region Variables

    private Resource thisResource;

    #endregion

    #region Methods

    private void Awake()
    {
        thisResource = GetComponent<Resource>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Animal animal = collision.GetComponent<Animal>();

        if (animal == null) { return; }

        if (animal.currentBehaviour is Hunger && animal.DoesThisAnimalInteractWithThisResource(thisResource))
        {
            animal.ConsumeResource(thisResource);
        }
    }

    #endregion

}
