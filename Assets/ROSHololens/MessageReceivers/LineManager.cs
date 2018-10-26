using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROSHololens
{
    public class LineManager : MonoBehaviour {
        LineRenderer lineRenderer;

        // Use this for initialization
        void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void Lines()
        {
            lineRenderer.startWidth = 0.02f;
            lineRenderer.endWidth = 0.02f;
            try
            {
                if (GlobalLine.vP != null)
                {
                    lineRenderer.positionCount = GlobalLine.vP.Length;
                    Debug.Log(lineRenderer.positionCount);
                    Debug.Log("Start");
                    //Debug.Log(GlobalLine.vP[0].x+ GlobalLine.vP[0].y+ GlobalLine.vP[0].z);
                    //Debug.Log(GlobalLine.vP[1].x + GlobalLine.vP[1].y + GlobalLine.vP[1].z);
                    //Debug.Log(GlobalLine.vP[2].x + GlobalLine.vP[2].y + GlobalLine.vP[2].z);

                    lineRenderer.SetPositions(GlobalLine.vP);
                }
                
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("big exception");
            }

        }
    }
}