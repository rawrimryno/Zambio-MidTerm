using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour {
    public float displayTime;
    float timeInSplash;
	// Use this for initialization
	void Start () {
	    if (displayTime < 2.0f)
        {
            displayTime = 2.0f;
        }
        timeInSplash = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
        timeInSplash += Time.deltaTime;
        if (timeInSplash >= displayTime)
        {
            // TODO:  Main Menu loads from Level One atm of Midterm demo,
            //  This should Load Main Menu by Final.  Main Menu will load level.
            SceneManager.LoadScene("Level One");
        }
    }
}
