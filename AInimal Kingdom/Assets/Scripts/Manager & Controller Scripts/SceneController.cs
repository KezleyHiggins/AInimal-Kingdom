using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    #region Accessing Scenes

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void GoToFreePlay()
    {
        SceneManager.LoadScene("Freeplay");
        Time.timeScale = 1;
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Time.timeScale = 1;
    }

    #endregion

}
