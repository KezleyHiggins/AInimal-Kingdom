using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    #region Variables

    SceneController sceneController;

    #endregion

    #region Awake, Start & Update

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #endregion

    #region Buttons

    public void OnCloseButtonPressed()
    {
        Application.Quit();
    }

    #endregion

}
