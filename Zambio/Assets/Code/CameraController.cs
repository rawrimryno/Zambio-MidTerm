using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Camera cam;
    private float camFOV;

	void Start () {
        camFOV = cam.fieldOfView;
	}
	
	void Update () {
	
        if(Input.GetButtonDown("zoom") && Time.timeScale == 1f) //Zoom In
        {
            cam.fieldOfView = 10f;
        }
        if(Input.GetButtonUp("zoom") &&  Time.timeScale == 1f) //Zoom Out
        {
            cam.fieldOfView = camFOV;
        }


	}
}
