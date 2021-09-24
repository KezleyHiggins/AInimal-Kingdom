using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSegment : MonoBehaviour
{

    #region Variables

    TutorialManager tutorialManager;
    CameraMovement player;
    SpriteRenderer sprite;
    [SerializeField] string segmentName, segmentText;

    #endregion

    void Awake()
    {
        tutorialManager = FindObjectOfType<TutorialManager>();
        player = FindObjectOfType<CameraMovement>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        OnEnterBounds();
    }

    public void OnEnterBounds()
    {
        if (sprite.bounds.Contains((Vector2)player.transform.position) == true)
        {
            tutorialManager.DisplayThisSegment(segmentName, segmentText);
        }
    }

}
