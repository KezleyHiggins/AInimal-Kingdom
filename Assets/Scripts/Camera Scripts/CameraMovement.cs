using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    #region Variable

    UIController uiController;
    Rigidbody2D rb;
    public bool isActive = true;
    public float moveSpeed;

    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        uiController = FindObjectOfType<UIController>();
    }

    protected virtual void Update()
    {
        if (isActive == true)
        {
            MoveCamera();
            ZoomCamera();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiController.pauseMenu.activeSelf == false)
            {
                uiController.PauseGame();
            }
            else if(uiController.pauseMenu.activeSelf == true && uiController.optionsMenu.activeSelf == false)
            {
                uiController.UnpauseGame();
            }
        }
    }

    #endregion

    #region Moving the camera

    protected void MoveCamera()
    {
        float vertical = (Input.GetAxis("Vertical") * moveSpeed);
        float horizontal = (Input.GetAxis("Horizontal") * moveSpeed);
        Vector2 cameraVelocity = new Vector2(horizontal, vertical);
        rb.velocity = cameraVelocity;
    }

    protected void ZoomCamera()
    {
        Vector3 newPos = transform.position;
        float zoomChange = Input.mouseScrollDelta.y;
        newPos.z += zoomChange;

        if (newPos.z < -42f) newPos.z = -42f;
        else if (newPos.z > -15f) newPos.z = -15f;

        transform.position = newPos;
    }

    #endregion

}
