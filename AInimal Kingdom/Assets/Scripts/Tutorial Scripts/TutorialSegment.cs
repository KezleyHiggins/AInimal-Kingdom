using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSegment : MonoBehaviour
{

    #region Variables
    TutorialManager tutorialManager;
    CameraMovement player;
    SpriteRenderer sprite;
    [SerializeField] GameObject segmentUI;
    #endregion

    #region Awake & Update

    private void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        player = FindObjectOfType<CameraMovement>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        OnEnterBounds();
    }

    #endregion

    #region Using This Segment's UI

    public void OnEnterBounds()
    {
        if (sprite.bounds.Contains((Vector2)player.transform.position) == true)
        {
            tutorialManager.TurnOffAllUI();
            TurnOnUI();
        }
        else
        {
            TurnOffUI();
        }
    }

    public void TurnOnUI()
    {
        segmentUI.SetActive(true);
    }

    public void TurnOffUI()
    {
        segmentUI.SetActive(false);
    }

    #endregion

}
