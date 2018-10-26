using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class SetHomeManger : MonoBehaviour, IInputClickHandler, IInputHandler {

    public GameObject homeIndicator;
    public GameObject rehomeableObject;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        homeIndicator.transform.position = rehomeableObject.transform.position;

    }
    public void OnInputDown(InputEventData eventData)
    {

    }
    public void OnInputUp(InputEventData eventData)
    { }
}
