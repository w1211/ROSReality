using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseToText : MonoBehaviour {

    public Text m_TextObject;

    public GameObject TargetObject;

    public float multiplier = 10;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_TextObject.text = "Position: " +  ((TargetObject.transform.position)  * multiplier).ToString();
    }
}
