using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using ROSHololens;

public enum Direction
{
    Forward, Backward, Left, Right, Stop
}


public class ChangeVelocity : MonoBehaviour, IInputClickHandler
{

    public VelocityManager velManager;
    public Direction direction;


    public void OnInputClicked(InputClickedEventData eventData)
    {
        switch (direction)
        {
            case Direction.Forward:
                velManager.SpeedUp();
                break;
            case Direction.Backward:
                velManager.SlowDown();
                break;
            case Direction.Left:
                velManager.TurnLeftMore();
                break;
            case Direction.Right:
                velManager.TurnRightMore();
                break;
            case Direction.Stop:
                velManager.Stop();
                break;
        }
    }
}
