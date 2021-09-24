using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {Meat, Fruit, Grass}

public class Resource : MonoBehaviour
{

    #region Variables

    public ResourceType resourceType;
    public int resourceValue;
    public bool isConsumed = false;
    public BoxCollider2D boxCollider;

    #endregion

    #region Awake

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    #endregion

}
