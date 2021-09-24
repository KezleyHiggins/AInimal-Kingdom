using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Source: https://youtu.be/HXFoUGw7eKk

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    static LTDescr delay;
    Image image;
    [SerializeField] Color highlightColour;
    Color normalColour;
    [SerializeField] string tooltipContent;

    void Awake()
    {
        image = GetComponent<Image>();
        normalColour = image.color;
    }

    void HighlightImage()
    {
        image.color = highlightColour;
    }

    void UnhighlightImage()
    {
        image.color = normalColour;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HighlightImage();
        delay = LeanTween.delayedCall(0.5f, () => { TooltipSystem.Show(tooltipContent); });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UnhighlightImage();
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();
    }

}
