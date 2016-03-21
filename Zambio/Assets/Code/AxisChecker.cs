using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AxisChecker : MonoBehaviour {
    Slider inputValueSlider;
	// Use this for initialization
	void Start () {
        inputValueSlider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        inputValueSlider.value = -1*Input.GetAxis("Fire1");
    }

    public void getValue()
    {
        //inputValueSlider.value = Input.GetAxis("Fire1");
    }
}
