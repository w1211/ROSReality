using ROSHololens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ROSHololens
{
    class MarkerReceiver : MessageReceiver
    {
        private Marker marker;
        public PositionManager positionManager;
        private Type type = typeof(Marker);
        public override Type MessageType { get { return type; } }
        private BeaconVisualizer[] beaconVisualizers;

        private bool isMessageReceived = false;

        private void Awake()
        {
            beaconVisualizers = GetComponents<BeaconVisualizer>();
        }

        public override void receiveMessage(Message m)
        {
            marker = (Marker)m;
            isMessageReceived = true;
            
        }

        private void Update()
        {
            if (isMessageReceived)
            {
                Debug.Log("marker revcieved");
                Vector3 markerMapCoords = GetPosition(marker.pose).Ros2Unity();
                Debug.Log(markerMapCoords); ;
                Vector3 unityCoords = positionManager.convertMapCoordinatesToUnityCoordinates(markerMapCoords);
                Debug.Log("converted");
                GeometryPoint location = new GeometryPoint();
                location.x = unityCoords.x;
                location.y = unityCoords.y;
                location.z = unityCoords.z;
                Debug.Log(location.x + ", " + location.y + ", " + location.z);
                marker.pose.position = location;

                if (beaconVisualizers != null)
                {
                    foreach (BeaconVisualizer beaconVisualizer in beaconVisualizers)
                    {
                        beaconVisualizer.addMarker(marker);
                    }
                }
                isMessageReceived = false;
            }
        }

        private Vector3 GetPosition(GeometryPose message)
        {
            return new Vector3(
                message.position.x,
                message.position.y,
                message.position.z);
        }

    }

}
