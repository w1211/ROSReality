using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TurtleInitialPositionLocatior : MonoBehaviour {


	// Use this for initialization
	void Start () {
        startVuforiaLocationSession();

    }

    public void startVuforiaLocationSession()
    {
        VuforiaBehaviour.Instance.enabled = true;
        foreach (Transform child in transform)
        {
            if (child.gameObject.name != "ImageTarget")
            {
                child.gameObject.SetActive(false);
            }
        }
    }
	

    public void robotLocated()
    {
        Debug.Log("preparing vuforia handover");
        VuforiaBehaviour.Instance.enabled = false;
        Debug.Log("disabled vuforia");
        foreach (Transform child in transform)
        {
            if (child.gameObject.name != "LaserScans")
            {
                child.gameObject.SetActive(true);
            }
           
        }

    }
	// Update is called once per frame
	void Update () {
		
	}
}
