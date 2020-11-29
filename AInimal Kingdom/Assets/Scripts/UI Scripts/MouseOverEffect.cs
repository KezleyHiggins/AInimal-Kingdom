using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    #region Variables

    public GameObject infoPanel;
    private Image image;
    public Color highlightColour, originalColour;

    #endregion

    #region Awake, Start & Update

    private void Start()
    {
        image = GetComponent<Image>();
        originalColour = image.color;
    }

    #endregion

    #region Panel Changes

    public void HighlightThisObject()
    {
        image.color = highlightColour;
    }

    void RemoveHightlightOnThisObject()
    {
        image.color = originalColour;
    }

    public void DisplayInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    #endregion

    #region Event systems

    public void OnPointerEnter(PointerEventData eventData)
    {
        HighlightThisObject();
        DisplayInfoPanel();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RemoveHightlightOnThisObject();
        HideInfoPanel();
    }

    #endregion

}
