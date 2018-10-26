using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSHololens;
using Vuforia;

public class PositionManager : MonoBehaviour {

    public PoseReceiver poseReceiver;

    public Vector3 turtlePositionUnity;
    public Quaternion turtleRotationUnity;

    public Vector3 turtlePreVuforiaTrackingLostPositionInMap;
    public Vector3 turtlePreVuforiaTrackingLostPositionInUnity;

    public Quaternion turtlePreVuforiaTrackingLostRotationInMap;
    public Quaternion turtlePreVuforiaTrackingLostRotationInUnity;



    public bool vuforiaActive = false;
    private TrackRobot vuforiaTracker;

    public void Start()
    {
        turtlePositionUnity = new Vector3(0, 0, 0);

        turtlePreVuforiaTrackingLostPositionInMap = new Vector3(0, 0, 0);
        turtlePreVuforiaTrackingLostPositionInUnity = new Vector3(0, 0, 0);

        turtlePreVuforiaTrackingLostRotationInUnity = new Quaternion(0, 0, 0, 1);
        turtlePreVuforiaTrackingLostRotationInMap = new Quaternion(0, 0, 0, 1);


        vuforiaTracker = GetComponentInChildren<TrackRobot>();
       
    }

    public Vector3 convertMapCoordinatesToUnityCoordinates(Vector3 coords)
    {
        Vector3 difference = transform.position - poseReceiver.getPosition();
        difference = (turtlePreVuforiaTrackingLostRotationInUnity * Quaternion.Inverse(turtlePreVuforiaTrackingLostRotationInMap)) * difference;
        return coords + difference;
    }

    public Vector3 fixVectorRotation(Vector3 orig)
    {
        return (turtlePreVuforiaTrackingLostRotationInUnity* Quaternion.Inverse(turtlePreVuforiaTrackingLostRotationInMap)) *orig;
    }

    public Vector3 convertMapCoordinatesToRelativeTurtleCoords(Vector3 coords)
    {
        Debug.Log("coords: " + coords);
        Debug.Log("poition: " + poseReceiver.getPosition());
        Vector3 difference = coords - poseReceiver.getPosition();
        return difference;
    }

    // Update is called once per frame
    public void Update()
    {
        if (poseReceiver.hasANewPosition)
        {
            
            Quaternion turtleRotationOffsetFromVuforia = turtlePreVuforiaTrackingLostRotationInMap * Quaternion.Inverse(poseReceiver.getRotation());
            Quaternion resultRotation = turtlePreVuforiaTrackingLostRotationInUnity * turtleRotationOffsetFromVuforia;


            //vuforia is not active so we'll get poses from the pose receiver if we can.
            Vector3 turtleOffsetFromVuforia = poseReceiver.getPosition() - turtlePreVuforiaTrackingLostPositionInMap;
            turtleOffsetFromVuforia = (turtlePreVuforiaTrackingLostRotationInUnity * Quaternion.Inverse(turtlePreVuforiaTrackingLostRotationInMap)) * turtleOffsetFromVuforia;
            Vector3 result =  (turtlePreVuforiaTrackingLostPositionInUnity + turtleOffsetFromVuforia);


            transform.rotation = resultRotation;
            transform.position = result;
        }
        
    }

    public void updatePositionFromVuforia()
    {
        Debug.Log("gotvuforia update");
        turtlePositionUnity = vuforiaTracker.transform.position;
        transform.position = turtlePositionUnity;

        turtleRotationUnity = vuforiaTracker.transform.rotation;
        transform.rotation = turtleRotationUnity;

        turtlePreVuforiaTrackingLostPositionInMap = poseReceiver.getPosition();
        turtlePreVuforiaTrackingLostPositionInUnity = turtlePositionUnity;

        turtlePreVuforiaTrackingLostRotationInMap = poseReceiver.getRotation();
        turtlePreVuforiaTrackingLostRotationInUnity = turtleRotationUnity;

    }

    /*public void VuforiaTrackingLost()
    {
        vuforiaActive = false;
        turtlePreVuforiaTrackingLostPositionInMap = poseReceiver.getPosition();
        turtlePreVuforiaTrackingLostPositionInUnity = turtlePositionUnity;

        turtlePreVuforiaTrackingLostRotationInMap = poseReceiver.getRotation();
        turtlePreVuforiaTrackingLostRotationInUnity = turtleRotationUnity;
    }
    */
        /*private Quaternion rotationOffset;
        private Vector3 positionOffset;
        private bool isInitalPose;

        private Quaternion zeroRotation;
        private Vector3 zeroPosition;

        public void Start()
        {
            positionOffset = new Vector3(0, 0, 0);
            rotationOffset = new Quaternion(0, 0, 0, 1);

        }

        // Update is called once per frame
        public void Update () {

            Vector3 curPos = pose.getPosePostion();
            Quaternion curRot = pose.getPoseRotation();

            if (isInitalPose)
            {
                zeroPosition = curPos;
                zeroRotation = curRot;

                isInitalPose = false;
            }



            transform.position = curPos + zeroPosition - positionOffset;
            transform.rotation = curRot;

        }
        public void reZeroPose(Vector3 newZeroPos, Quaternion newZeroRot)
        {
            zeroPosition = newZeroPos;
            zeroRotation = newZeroRot;

            //positionOffset = transform.position - ;
            rotationOffset = transform.rotation; // - zeroRotation;

        }*/



    }
