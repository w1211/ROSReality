using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSHololens;


 public class VelocityManager : MonoBehaviour {

    public TeleopPublisher teleopPublisher;

    //public TeleopPublisher publisher;

    public static float maxLinearChange = 0.01f;
    public static float maxAngularChange = 0.1f;


    public static float maxLinearSpeed =  0.26f;
    public static float maxAngularSpeed = 1.82f;


    // Use this for initialization
    void Start () {
      

    }
	
    public void SpeedUp()
    {
        if(teleopPublisher.message.linear.x <= maxLinearSpeed) teleopPublisher.message.linear.x += maxLinearChange;
        teleopPublisher.publishMessage();
    }

    public void SlowDown()
    {
        if (teleopPublisher.message.linear.x >= -maxLinearSpeed) teleopPublisher.message.linear.x -= maxLinearChange;
        teleopPublisher.publishMessage();
    }

    public void TurnLeftMore()
    {

        if (teleopPublisher.message.angular.z <= maxAngularSpeed) teleopPublisher.message.angular.z += maxAngularChange;
        teleopPublisher.publishMessage();
    }

    public void TurnRightMore()
    {
        if (teleopPublisher.message.angular.z >= -maxAngularSpeed) teleopPublisher.message.angular.z -= maxAngularChange;
        teleopPublisher.publishMessage();
    }

    public void Stop()
    {
        teleopPublisher.message.linear = new GeometryVector3();
        teleopPublisher.message.angular = new GeometryVector3();
        teleopPublisher.publishMessage();
    }

}