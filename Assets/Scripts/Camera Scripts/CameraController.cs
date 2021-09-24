using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    #region Variables

    [SerializeField] GameObject focusedObject;
    [SerializeField] GameObject playerCameraViewer;
    [SerializeField] float lerpSpeed = 10f;

    #endregion

    #region Awake, Start & Update

    void Update()
    {
        if (focusedObject == null) focusedObject = playerCameraViewer;

        SetNewCameraPosition();
    }

    #endregion

    #region Setting Camera Position & Focus

    void SetNewCameraPosition()
    {
        Vector3 newPosition = focusedObject.transform.position;
        if (newPosition.z != -15f && focusedObject != playerCameraViewer) newPosition.z = -15f;

        if (focusedObject != playerCameraViewer)
        {
            transform.position = newPosition;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, lerpSpeed * Time.deltaTime);
        }
    }

    public void SwitchFocusedObject(GameObject newFocus)
    {
        focusedObject = newFocus;
    }

    public void RemoveFocusedObject()
    {
        Vector3 newPos = focusedObject.transform.position;
        newPos.z = -15f;

        playerCameraViewer.transform.position = newPos;

        focusedObject = null;
    }

    #endregion

}
