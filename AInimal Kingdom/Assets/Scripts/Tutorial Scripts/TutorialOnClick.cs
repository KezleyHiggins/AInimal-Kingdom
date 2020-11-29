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
    
    private void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    private void OnMouseDown()
    {
        Debug.Log("Yeet");
        cameraController.SwitchFocusedObject(gameObject);
        tutorialUI.SetActive(true);
    }

    #endregion
    
}
