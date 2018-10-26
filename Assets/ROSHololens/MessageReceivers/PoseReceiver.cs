using ROSHololens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ROSHololens
{
    public class PoseReceiver : MessageReceiver
    {
        private Type type = typeof(GeometryPose);
        public override Type MessageType { get { return type; } }

        private Vector3 position;
        private Quaternion rotation;

        private Transform messageTansform;

        public bool isMessageReceived;


        public bool hasANewPosition = false;


        public override void receiveMessage(Message m)
        {
            position = GetPosition((GeometryPose)m).Ros2Unity();
            //Debug.Log("got position");
            rotation = GetRotation((GeometryPose)m).Ros2Unity();
            isMessageReceived = true;
            hasANewPosition = true;
            //Debug.Log("pose reciever got pose");
        }

       /* private void Update()
        {
            if (isMessageReceived)
            {
                transform.position = position;
                transform.rotation = rotation;
            }
            isMessageReceived = false;
        }*/

        public Vector3 getPosition()
        {
            return position;
        }

        public Quaternion getRotation()
        {
            return rotation;
        }


        public Vector3 getPosePostion()
        {
            return position;
        }
        public Quaternion getPoseRotation()
        {
            return rotation;
        }

        private Vector3 GetPosition(GeometryPoseStamped message)
        {
            return new Vector3(
                message.pose.position.x,
                message.pose.position.y,
                message.pose.position.z);
        }
        private Vector3 GetPosition(GeometryPose message)
        {
            return new Vector3(
                message.position.x,
                message.position.y,
                message.position.z);
        }


        private Vector3 GetPosition(NavigationOdometry message)
        {
            return new Vector3(
                message.pose.pose.position.x,
                message.pose.pose.position.y,
                message.pose.pose.position.z);
        }

        private Quaternion GetRotation(GeometryPoseStamped message)
        {
            return new Quaternion(
                message.pose.orientation.x,
                message.pose.orientation.y,
                message.pose.orientation.z,
                message.pose.orientation.w);
        }


        private Quaternion GetRotation(GeometryPose message)
        {
            return new Quaternion(
                message.orientation.x,
                message.orientation.y,
                message.orientation.z,
                message.orientation.w);
        }

        private Quaternion GetRotation(NavigationOdometry message)
        {
            return new Quaternion(
                message.pose.pose.orientation.x,
                message.pose.pose.orientation.y,
                message.pose.pose.orientation.z,
                message.pose.pose.orientation.w);
        }

    }
   
}
