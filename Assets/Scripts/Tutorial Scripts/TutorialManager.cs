using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI segmentName, segmentText;

    public void DisplayThisSegment(string _segmentName, string _segmentText)
    {
        segmentName.text = _segmentName;
        segmentText.text = _segmentText;
    }

}
