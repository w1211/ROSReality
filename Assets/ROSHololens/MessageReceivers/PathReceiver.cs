using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ROSHololens
{
    public class GlobalLine
    {
        public static Vector3[] vP;
    }

    public class PathReceiver : MessageReceiver
    {

        NavigationPath path;
        public PositionManager positionMan;
        // Subscribing to navigation path
        private Type type = typeof(NavigationPath);
        public override Type MessageType { get { return type; } }
        private bool isMessageReceived;
        private LineManager[] pathVisualizers;

        public override void receiveMessage(Message m)
        {
            path = (NavigationPath)m;
            isMessageReceived = true;
        }

        private void Awake()
        {
            pathVisualizers = GetComponents<LineManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isMessageReceived)
            {
                Debug.Log("Received Message for TESTLINE");
                GeometryPoseStamped[] recvPoses = (path).poses;
                int lenArr = recvPoses.Length;
                Debug.Log(lenArr); // Length = number of objects
                GlobalLine.vP = new Vector3[lenArr];
                Vector3 startingPosition = positionMan.fixVectorRotation(GetPosition((GeometryPoseStamped)recvPoses[0]).Ros2Unity());
                for (int i = 0; i < lenArr; i++)
                {
                    //Vector3 position = positionMan.convertMapCoordinatesToUnityCoordinates(GetPosition((GeometryPoseStamped)recvPoses[i]).Ros2Unity());
                    Vector3 basePosition = positionMan.fixVectorRotation(GetPosition((GeometryPoseStamped)recvPoses[i]).Ros2Unity());
                    Vector3 position = startingPosition - basePosition;

                    GlobalLine.vP[i][0] = position.x; //x
                    GlobalLine.vP[i][1] =  position.y; //y
                    GlobalLine.vP[i][2] =  position.z; //z

                    if (pathVisualizers != null)
                    {
                        foreach (LineManager lineManager in pathVisualizers)
                        {
                            lineManager.Lines();
                        }
                    }

                }

                //Debug.Log(GlobalLine.vP[0].x + GlobalLine.vP[0].y + GlobalLine.vP[0].z);
                //Debug.Log(GlobalLine.vP[1].x + GlobalLine.vP[1].y + GlobalLine.vP[1].z);
                //Debug.Log(GlobalLine.vP[2].x + GlobalLine.vP[2].y + GlobalLine.vP[2].z);
                
                isMessageReceived = false;
            }
        }

        private Vector3 GetPosition(GeometryPoseStamped message)
        {
            return new Vector3(
                message.pose.position.x,
                message.pose.position.y,
                message.pose.position.z);
        }

        private Quaternion GetRotation(GeometryPoseStamped message)
        {
            return new Quaternion(
                message.pose.orientation.x,
                message.pose.orientation.y,
                message.pose.orientation.z,
                message.pose.orientation.w);
        }
    }
}