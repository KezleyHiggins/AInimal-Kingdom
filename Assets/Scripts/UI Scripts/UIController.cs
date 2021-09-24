using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{

    #region Variables

    private GameManager gameManager;
    [Header("General Variables")]
    public Animal focusedAnimal;
    public GameObject AnimalViewer;
    private CameraController cameraController;
    [Header("Left Panel")]
    public TextMeshProUGUI hungerText;
    public TextMeshProUGUI currentAreaText, currentRegionText, sleepDuringText, currentlyDoingText;
    [Header("Center Panel")]
    public TextMeshProUGUI animalNameText;
    public TextMeshProUGUI animalSpeciesText;
    [Header("Right Panel")]
    public TextMeshProUGUI currentBehaviourText;
    public TextMeshProUGUI WhyAmIDoingThisBehaviourText, DescriptionOfCurrentBehaviourText;
    [Header("Pause & Options Menu")]
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        cameraController = FindObjectOfType<CameraController>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (focusedAnimal != null && focusedAnimal.gameObject.activeSelf == true)
        {
            UpdateLeftPanelUI();
            UpdateCenterPanelUI();
            UpdateRightPanelUI();
        }
        if (focusedAnimal != null)
        {
            if (focusedAnimal.gameObject.activeSelf == false)
            {
                focusedAnimal = null;
            }
        }
    }

    #endregion

    #region Accessing UI
    public void OpenAnimalViewer(Animal newAnimal)
    {
        focusedAnimal = newAnimal;
        AnimalViewer.SetActive(true);
        SetCameraToFollowAnimal(newAnimal);
    }

    public void CloseAnimalViewer()
    {
        focusedAnimal = null;
        AnimalViewer.SetActive(false);
        SetCameraToPlayerControl();
    }

    #endregion

    #region Left Panel
    void UpdateLeftPanelUI()
    {
        UpdateHungerText();
        UpdateLocationText();
        UpdateSleepingText(); 
        UpdateCurrentlyDoingText(); 
    }

    private void UpdateHungerText()
    {
        hungerText.text = "Hunger: " + focusedAnimal.hunger.ToString();
    }

    private void UpdateLocationText()
    {
        currentAreaText.text = "Current Area: "+focusedAnimal.regionIAmIn.areaThisRegionIsIn.areaName;
        currentRegionText.text = "Current Region: "+focusedAnimal.regionIAmIn.regionName;
    }

    private void UpdateSleepingText()
    {
        var sleepDuring = focusedAnimal.sleepBehaviour.sleepDuring;
        var currentTimeOfDay = focusedAnimal.sleepBehaviour.currentTimeOfDay;
        sleepDuringText.text = "I sleep during " + sleepDuring + " and it is " + currentTimeOfDay;
    }

    private void UpdateCurrentlyDoingText()
    {
        currentlyDoingText.text = focusedAnimal.currentBehaviour.GetCurrentlyDoingText();
    }

    #endregion

    #region Center Panel

    void UpdateCenterPanelUI()
    {
        animalNameText.text = "Animal Name: " + focusedAnimal.animalName;
        animalSpeciesText.text = "Animal Species: " + focusedAnimal.animalType;
    }

    #endregion

    #region Right Panel

    void UpdateRightPanelUI()
    {
        UpdateDescriptionOfCurrentBehaviourText();
        UpdateWhyAmIDoingThisText();
    }

    private void UpdateDescriptionOfCurrentBehaviourText()
    {
        string currentBehaviourName = "The " + focusedAnimal.currentBehaviour.GetAIBehaviourName() + " behaviour is currently active.";
        string currentBehaviourDescription = focusedAnimal.currentBehaviour.GetDescriptionOfCurrentBehaviour();
        DescriptionOfCurrentBehaviourText.text = currentBehaviourName+ " " + currentBehaviourDescription;
    }

    private void UpdateWhyAmIDoingThisText()
    {
        WhyAmIDoingThisBehaviourText.text = focusedAnimal.currentBehaviour.GetWhyAmIDoingThisText();
    }

    #endregion

    #region Pause Menu

    public void PauseGame()
    {
        gameManager.PauseGame();
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        gameManager.UnpauseGame();
        pauseMenu.SetActive(false);
    }

    #endregion

    #region Utility

    private void SetCameraToFollowAnimal(Animal animalToFollow)
    {
        cameraController.SwitchFocusedObject(animalToFollow.gameObject);
    }

    private void SetCameraToPlayerControl()
    {
        cameraController.RemoveFocusedObject();
    }

    #endregion

}
