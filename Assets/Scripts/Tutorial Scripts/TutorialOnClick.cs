using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOnClick : MonoBehaviour
{

    #region Variables

    CameraController cameraController;
    [SerializeField] GameObject tutorialUI;

    #endregion

    #region Methods
    
    void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    void OnMouseDown()
    {
        cameraController.SwitchFocusedObject(gameObject);
        tutorialUI.SetActive(true);
    }

    #endregion
    
}
