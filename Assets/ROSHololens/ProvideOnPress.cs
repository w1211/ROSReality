using System;
using System.Collections.Generic;
using UnityEngine;
using ROSHololens;
using HoloToolkit.Unity.InputModule;

namespace ROSHololens {
    public class ProvideOnPress : MonoBehaviour, IInputClickHandler
    {
        
        public GameObject target;

        private GeometryPoseStamped message;


        public PosePublisher publisher;

        public Type MessageType { get { return (typeof(GeometryPoseStamped)); } }

        private void Start()
        {
            InitializeMessage();

        }


        private void InitializeMessage()
        {
            message = new GeometryPoseStamped();
            message.header = new StandardHeader();
            message.pose = new GeometryPose();
            message.pose.orientation = new GeometryQuaternion();
            message.pose.orientation.w = 1;

            message.header.frame_id = "map";
            message.header.stamp = new StandardTime();
           // RaiseMessageRelease(new MessageEventArgs(message));
        }



        private void UpdateMessage()
        {
           


            message.pose.position.x = target.transform.position.x;
            message.pose.position.y = target.transform.position.z; // Y and Z swapped ros <-> unity
            //RaiseMessageRelease(new MessageEventArgs(message));
        }

        public void OnInputClicked(InputClickedEventData eventData)
        {
            message.header.stamp.secs = (int)DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            Vector3 point =  TransformExtensions.Unity2Ros(target.transform.position);
           // point = Quaternion.Euler(0, -45, 0) * point;
            point.z = 0;


            message.pose.position.x = point.x;
            message.pose.position.y = point.y;
            message.pose.position.z = point.y;

            publisher.message = message;
            publisher.publishMessage();


        }

    }
}
