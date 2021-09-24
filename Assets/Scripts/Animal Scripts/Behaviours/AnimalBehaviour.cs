using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Animal))]
public abstract class AnimalBehaviour : MonoBehaviour
{
    protected Animal thisAnimal;

    private void Awake()
    {
        thisAnimal = GetComponent<Animal>();
    }

    public abstract bool IsThisBehaviourTriggered();

    public abstract void DoBehaviour();

    public abstract string GetCurrentlyDoingText();

    public abstract string GetWhyAmIDoingThisText();

    public abstract string GetAIBehaviourName();

    public abstract string GetDescriptionOfCurrentBehaviour();

}
