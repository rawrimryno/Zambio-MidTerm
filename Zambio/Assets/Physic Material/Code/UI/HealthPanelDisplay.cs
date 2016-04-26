using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthPanelDisplay : MonoBehaviour {
    Text myTextBox;
    GameControllerSingleton gc;
    public HealthPanel myParent;
    Image myImage;

    void Awake()
    {
        myTextBox = GetComponent<Text>();
    }
	// Use this for initialization
	void Start () {
        gc = GameControllerSingleton.get();
        myTextBox.text = gc.getAmmoDisplayNamefromID(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setTextToAmmoName()
    {
        // Zach's bullet counter starts at 1, our database starts at 0, hence offset.
        myTextBox.text = gc.getAmmoDisplayNamefromID(myParent.bullet - 1);
    }
}
