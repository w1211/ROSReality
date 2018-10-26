using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSender : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("started");
        StartCoroutine(Example());
    }



    IEnumerator Example()
    {
        Debug.Log("hi");
        yield return new WaitForSeconds(5);
        Debug.Log("fading!");
        Initiate.Fade("MainScene", Color.black, 5.0f);
    }

    // Update is called once per frame
    void Update () {

	}
}
