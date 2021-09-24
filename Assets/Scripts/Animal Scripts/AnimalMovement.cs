using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementDirection {N, E, S, W, NW, NE, SW, SE, None}

[RequireComponent(typeof(Animal))]
public class AnimalMovement : MonoBehaviour
{

    #region Variables

    [HideInInspector] public MovementDirection movementDirection = MovementDirection.None;
    Rigidbody2D rb;
    TimeManager timeManager;
    [Header("General Speed Settings")]
    [SerializeField] float moveSpeed;
    float startSpeed;
    Animal thisAnimal;
    [HideInInspector] public Vector3 currentTarget;
    public float minClosenessThreshold;
    public bool hasMovementTarget = false;
    [Header("Animal Speed Thresholds")]
    public float hungerLowestSpeedThreshold;
    public float hungerMidSpeedThreshold, hungerMaxSpeedThreshold;
    [Header("Animal Hunger Speed Bonus")]
    public float lowestHungerSpeed;
    public float midHungerSpeed, maxHungerSpeed;

    #endregion

    #region Awake, Start & Update

    void Awake()
    {
        thisAnimal = GetComponent<Animal>();
        rb = GetComponent<Rigidbody2D>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    void Start()
    {
        startSpeed = moveSpeed;
    }

    void Update()
    {
        UpdateAnimalSpeed();
        HandleMovement();
        DetermineMovementDirection();
    }

    #endregion

    #region Setting Animal Speed

    private void UpdateAnimalSpeed()
    {
        moveSpeed = (startSpeed + GetHungerModifier()) * GetGameSpeedModifier();
    }

    private float GetHungerModifier()
    {
        float hunger = thisAnimal.hunger;

        if (hunger > hungerLowestSpeedThreshold && hunger < hungerMidSpeedThreshold)
        {
            return lowestHungerSpeed;
        }
        if (hunger > hungerMidSpeedThreshold && hunger < hungerMaxSpeedThreshold)
        {
            return midHungerSpeed;
        }
        else if (hunger > hungerMaxSpeedThreshold)
        {
            return maxHungerSpeed;
        }

        return 0;
    }

    private float GetGameSpeedModifier()
    {
        float speedModifier = 1f;

        if (timeManager.timeScale == TimeScale.Normal)
        {
            speedModifier = 2f;
        }
        else if (timeManager.timeScale == TimeScale.Fast)
        {
            speedModifier = 3f;
        }

        return speedModifier;
    }

    #endregion

    #region Revised Movement System

    void HandleMovement()
    {
        if (hasMovementTarget == false) rb.velocity = Vector2.zero;
        else if (ShouldIBeMoving() == true) MoveTowardsCurrentTarget();
    }

    bool ShouldIBeMoving()
    {
        if (hasMovementTarget == false) { return false; }

        if (AmICloseEnoughToMyTarget() == true)
        {
            return false;
        }
        return true;
    }

    bool AmICloseEnoughToMyTarget()
    {
        if (Vector3.Distance(transform.position, currentTarget) < 0)
        {
            ResetMovementTarget();
            return true;
        }
        return false;
    }

    void MoveTowardsCurrentTarget()
    {
        if (hasMovementTarget == false) { return; }

        rb.MovePosition(Vector3.MoveTowards(transform.position, currentTarget, moveSpeed * Time.deltaTime));
    }


    #endregion

    #region Setting Movement Target

    public void MoveToRandomLocationInThisArea(Area area)
    {
        Region regionToMoveTo = area.GetRandomRegionWithinThisArea();
        currentTarget = regionToMoveTo.GetRandomPositionWithinThisRegion();
        hasMovementTarget = true;
    }

    public void MoveTowardsThis(Region region)
    {
        currentTarget = region.centerOfRegion.position;
        hasMovementTarget = true;
    }

    public void MoveTowardsThis(Resource resource)
    {
        currentTarget = resource.transform.position;
        hasMovementTarget = true;
    }

    public void MoveTowardsThis(Animal animal)
    {
        currentTarget = animal.transform.position;
        hasMovementTarget = true;
    }

    public void MoveTowardsThis(Vector3 newPosition)
    {
        currentTarget = newPosition;
        hasMovementTarget = true;
    }

    #endregion

    #region Utility

    public void ResetMovementTarget()
    {
        hasMovementTarget = false;
    }

    void DetermineMovementDirection()
    {
        Vector3 velocity = rb.velocity;
        if (velocity.y > 0)
        {
            DetermineTypeOfNorth();
        }
        else if (velocity.y < 0)
        {
            DetermineTypeOfSouth();
        }
        else
        {
            DetermineEastOrWest();
        }
    }

    void DetermineTypeOfNorth()
    {
        Vector3 velocity = rb.velocity;
        if (velocity.x == 0)
        {
            movementDirection = MovementDirection.N;
        }
        else if (velocity.x > 0)
        {
            movementDirection = MovementDirection.NE;
        }
        else if (velocity.x < 0)
        {
            movementDirection = MovementDirection.NW;
        }
    }

    void DetermineTypeOfSouth()
    {
        Vector3 velocity = rb.velocity;

        if (velocity.x == 0)
        {
            movementDirection = MovementDirection.S;
        }
        else if (velocity.x > 0)
        {
            movementDirection = MovementDirection.SE;
        }
        else if (velocity.x < 0)
        {
            movementDirection = MovementDirection.SW;
        }
    }

    void DetermineEastOrWest()
    {
        Vector3 velocity = rb.velocity;

        if (velocity.x > 0)
        {
            movementDirection = MovementDirection.E;
        }
        else
        {
            movementDirection = MovementDirection.W;
        }
    }

    #endregion

    #region Animal Collision

    void OnCollisionEnter2D(Collision2D collision)
    {
        Animal collidedAnimal = collision.gameObject.GetComponent<Animal>();

        if (collidedAnimal == null) { return; }

        if (IsThisAnimalMovingInMyOppositeDirection(collidedAnimal) == true)
        {
            // change layer
            gameObject.layer = 12;
            StartCoroutine("ReturnSortingLayerToNormalAfterDelay");
        }

    }

    IEnumerator ReturnSortingLayerToNormalAfterDelay()
    {
        yield return new WaitForSeconds(4f);
        gameObject.layer = 10;
    }

    bool IsThisAnimalMovingInMyOppositeDirection(Animal collidedAnimal)
    {
        var otherMovementDirection = collidedAnimal.movement.movementDirection;
        bool northSouth = (otherMovementDirection == MovementDirection.N && movementDirection == MovementDirection.S || (otherMovementDirection == MovementDirection.S && movementDirection == MovementDirection.N));
        bool northEastSouthWest = (otherMovementDirection == MovementDirection.NE && movementDirection == MovementDirection.SW || otherMovementDirection == MovementDirection.SW && movementDirection == MovementDirection.NE);
        bool northWestSouthEast = (otherMovementDirection == MovementDirection.NW && movementDirection == MovementDirection.SE || otherMovementDirection == MovementDirection.SE && movementDirection == MovementDirection.NW);
        bool eastWest = (otherMovementDirection == MovementDirection.E && movementDirection == MovementDirection.W || (otherMovementDirection == MovementDirection.W && movementDirection == MovementDirection.E));

        if (northSouth == true)
        {
            return true;
        }
        else if (northEastSouthWest == true)
        {
            return true;
        }
        else if (northWestSouthEast == true)
        {
            return true;
        }
        else if (eastWest == true)
        {
            return true;
        }

        return false;
    }

    #endregion

}
