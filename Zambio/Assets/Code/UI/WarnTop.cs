using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WarnTop : MonoBehaviour {

    public Text warningOBJ;
    public string warningTXT;

	void Start () {
	
	}
	
	void Update () {
        if (Input.GetKeyUp("m"))
        {
            setWarningText();
        }
    }

    public void setWarningText()
    {
        warningOBJ.text = warningTXT;
    }

}
