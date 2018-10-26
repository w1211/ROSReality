using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ROSHololens
{
    public class TeleopPublisher : Publisher
    {

        private float update;


        public static float maxLinearChange = 0.01f;
        public static float maxAngularChange = 0.1f;


        public static float maxLinearSpeed = 0.26f;
        public static float maxAngularSpeed = 1.82f;

        public GeometryTwist message;

        public override Type MessageType { get { return (typeof(GeometryTwist)); } }

        public override void publishMessage()
        {
            GetComponent<RosConnector>().Publish(this, message);

        }



        private void Start()
        {
            InitializeMessage();
        }

        private void InitializeMessage()
        {
            message = new GeometryTwist();
            message.linear = new GeometryVector3();
            message.angular = new GeometryVector3();
        }


        public void SpeedUp()
        { 
            if (message.linear.x <= maxLinearSpeed) message.linear.x += maxLinearChange;
            publishMessage();
        }

        public void SlowDown()
        {
            if (message.linear.x >= -maxLinearSpeed) message.linear.x -= maxLinearChange;
            publishMessage();
        }

        public void TurnLeftMore()
        {

            if (message.angular.z <= maxAngularSpeed) message.angular.z += maxAngularChange;
            publishMessage();
        }

        public void TurnRightMore()
        {
            if (message.angular.z >= -maxAngularSpeed) message.angular.z -= maxAngularChange;
            publishMessage();
        }

        public void Stop()
        {
            message.linear = new GeometryVector3();
            message.angular = new GeometryVector3();
        }

    }





}
