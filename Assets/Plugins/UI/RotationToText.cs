using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotationToText : MonoBehaviour {

    public Text m_TextObject;

    public GameObject TargetObject;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_TextObject.text = "Rotation: " + (TargetObject.transform.rotation).ToString();
    }
}
