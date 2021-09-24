using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Source: https://youtu.be/HXFoUGw7eKk

public class TooltipSystem : MonoBehaviour
{
    static TooltipSystem current;
    public Tooltip tooltip;

    void Awake()
    {
        current = this;
        Hide();
    }

    public static void Show(string content)
    {
        current.tooltip.gameObject.SetActive(true);
        current.tooltip.SetText(content);
        current.tooltip.FadeIn();
    }

    public static void Hide()
    {
        current.tooltip.SetAlphaToZero();
        current.tooltip.gameObject.SetActive(false);
    }
}
