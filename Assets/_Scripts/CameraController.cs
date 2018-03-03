using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var camera = GetComponent<Camera>();
	    camera.transparencySortMode = TransparencySortMode.CustomAxis;
        camera.transparencySortAxis = new Vector3(0, 0.1f, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
