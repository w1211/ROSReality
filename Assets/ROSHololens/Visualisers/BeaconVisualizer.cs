using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace ROSHololens
{
    class BeaconVisualizer : MonoBehaviour
    {

        private Dictionary<int, GameObject> markers = new Dictionary<int, GameObject>();
        private List<Marker> toAdd = new List<Marker>();
        private List<Marker> toUpdate = new List<Marker>();
        public GameObject marker;
        public bool mustInit = false;
        public bool mustUpdate = false;

        private void Update()
        {
            if (mustInit)
            {
                for (int i = toAdd.Count - 1; i >= 0; i--)
                {

                    Marker m = toAdd[i];
                    markers[m.id] = (GameObject)Instantiate(marker,  parent: transform, instantiateInWorldSpace: true);
                    markers[m.id].transform.position = TransformExtensions.GetPosition(m.pose);
                    Debug.Log(TransformExtensions.GetPosition(m.pose));
                    markers[m.id].transform.rotation = GetRotation(m.pose).Ros2Unity();
                    markers[m.id].GetComponent<Renderer>().material.color = m.color.toColor();
                    toAdd.RemoveAt(i);

                    TextToSpeechManager.Instance.beaconDetected();
                }

                mustInit = false;
            }

            if (mustUpdate)
            {
                for (int i = toUpdate.Count - 1; i >= 0; i--)
                {
                    Marker m = toUpdate[i];
                    markers[m.id].transform.localPosition = TransformExtensions.GetPosition(m.pose);
                    markers[m.id].GetComponent<Renderer>().material.color = m.color.toColor();
                    Debug.Log("updated m");
                }

                mustUpdate = false;
            }
        }

        public void addMarker(Marker m)
        {
            
            if (markers.ContainsKey(m.id))
            {
                toUpdate.Add(m);
                mustUpdate = true;
            } else
            {
                toAdd.Add(m);
                mustInit = true;
            }
            Debug.Log("added marker!!!");
        }

        private Quaternion GetRotation(GeometryPose message)
        {
            return new Quaternion(
                message.orientation.x,
                message.orientation.y,
                message.orientation.z,
                message.orientation.w);
        }
    }

    
}
