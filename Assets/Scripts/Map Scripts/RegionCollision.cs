using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionCollision : MonoBehaviour
{

    #region Variables

    private Region thisRegion;

    #endregion

    #region Awake

    private void Awake()
    {
        thisRegion = GetComponent<Region>();
    }

    #endregion

    #region On Trigger Methods

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Animal animal = collision.GetComponent<Animal>();

        if (animal == null || animal.regionIAmIn == this) { return; }

        animal.regionIAmIn = thisRegion;
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Animal animal = collision.GetComponent<Animal>();

        if (animal == null || animal.regionIAmIn == this) { return; }

        animal.regionIAmIn = thisRegion;
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        Animal animal = collision.GetComponent<Animal>();

        if (animal == null || animal.regionIAmIn != this) { return; }

        animal.regionIAmIn = null;
    }

    #endregion

}
