using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            pos.z += 0.05f;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= 0.05f;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= 0.05f;
            transform.position = pos;
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += 0.05f;
            transform.position = pos;
        }
    }
}
