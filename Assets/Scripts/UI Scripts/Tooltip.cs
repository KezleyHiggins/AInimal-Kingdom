using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// Source: https://youtu.be/HXFoUGw7eKk

public class Tooltip : MonoBehaviour
{
    CanvasGroup tooltipCanvasGroup;
    TextMeshProUGUI contentField;
    LayoutElement layoutElement;
    RectTransform rectTransform;
    [SerializeField] int characterWrapLimit;

    void Awake()
    {
        tooltipCanvasGroup = GetComponent<CanvasGroup>();
        layoutElement = GetComponent<LayoutElement>();
        contentField = GetComponentInChildren<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        int contentLength = contentField.text.Length;
        layoutElement.enabled = (contentLength > characterWrapLimit);

        Vector2 position = Input.mousePosition;
        transform.position = position;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
    }

    public void SetText(string content)
    {
        contentField.text = content;
    }

    public void FadeIn()
    {
        LeanTween.alphaCanvas(tooltipCanvasGroup, 1, 0.25f);        
    }

    public void SetAlphaToZero()
    {
        if (tooltipCanvasGroup == null) tooltipCanvasGroup = GetComponent<CanvasGroup>();
        tooltipCanvasGroup.alpha = 0;
    }
}

