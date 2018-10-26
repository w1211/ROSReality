using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.EventSystems;
using HoloToolkit.Unity.InputModule;
using HoloToolkit;

using ROSHololens;
public class RepositionRobot : MonoBehaviour, IInputClickHandler
{

    public PositionManager positionManager;


    public GameObject centerOn;


    public void OnInputClicked(InputClickedEventData eventData)
    {

        //positionManager.reZeroPose(centerOn.transform.position, centerOn.transform.rotation);
    }
}


