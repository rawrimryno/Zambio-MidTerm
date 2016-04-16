using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoTab : MonoBehaviour {

    public GameObject infoTab;
    public GameObject ui;
    public bool active;

	void Start () {
        ui.SetActive(false);
        toggleInfo();
    }
	
	void Update () {
        if (Input.GetKeyDown("tab") && Time.timeScale == 1f)
        {
            if (active)
            {
                active = false;
            }
            else
            {
                active = true;
            }
            toggleInfo();
        }
    }

    public void toggleInfo ()
    {
        infoTab.SetActive(active);
    }

    public void displayControls()
    {
        
    }

}
