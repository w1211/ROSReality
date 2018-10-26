using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ROSHololens
{
    public class PosePublisher : Publisher
    {
        public GeometryPoseStamped message;
        public override Type MessageType { get { return (typeof(GeometryPoseStamped)); } }
        private float update;

        public override void publishMessage()
        {
            GetComponent<RosConnector>().Publish(this, message);
        }


        private void Start()
        {
            InitializeMessage();
        }

        private void UpdateMessage()
        {
            message.header.Update();
            message.pose.position = GetGeometryPoint(transform.position.Unity2Ros());
            message.pose.orientation = GetGeometryQuaternion(transform.rotation.Unity2Ros());
        }

        private GeometryPoint GetGeometryPoint(Vector3 position)
        {
            GeometryPoint geometryPoint = new GeometryPoint();
            geometryPoint.x = position.x;
            geometryPoint.y = position.y;
            geometryPoint.z = position.z;
            return geometryPoint;
        }
        private GeometryQuaternion GetGeometryQuaternion(Quaternion quaternion)
        {
            GeometryQuaternion geometryQuaternion = new GeometryQuaternion();
            geometryQuaternion.x = quaternion.x;
            geometryQuaternion.y = quaternion.y;
            geometryQuaternion.z = quaternion.z;
            geometryQuaternion.w = quaternion.w;
            return geometryQuaternion;
        }

        private void InitializeMessage()
        {
            message = new GeometryPoseStamped();
            message.header = new StandardHeader();
        }
    }





}
