using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject ui;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject bossBarOBJ;
    public GameObject creditsOBJ;

    public CursorLockMode cursorLock;

    private bool focus = true;
    private bool focusLock = true;

    //public Button[] mmBTN;
    //public Button[] pmBTN;
    //private int mainBTN = -1;
    //private int pauseBTN = -1;

    GameControllerSingleton gc;

    void Start () {
        Time.timeScale = 0f;

        cursorLock = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        gc = GameControllerSingleton.get();
    }
	
	void Update () {
        if (Input.GetButtonDown("Pause") && !creditsOBJ.activeInHierarchy)
        {
            onPause();
        }
        if (!focus && focusLock && Time.timeScale == 1.0f && !creditsOBJ.activeInHierarchy) //Pauses Game on Focus Loss
        {
            onPause();
        }

        //Debug Code
        if (Input.GetKeyUp("p"))
        {
            bossBarOBJ.SetActive(true);
        }
	}

    void OnApplicationFocus(bool focusStatus)
    {
        focus = focusStatus;
        if (focus)
            focusLock = false;
        else
            focusLock = true;
    }

    public void onPlay()
    {
        mainMenu.SetActive(false);
        ui.SetActive(true);

        Time.timeScale = 1f;
        cursorLock = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void onQuit()
    {
        Application.Quit();
    }

    public void onMenu()
    {
        gc.pc.deathSequence();

        Time.timeScale = 0f;
        Cursor.visible = true;
        cursorLock = CursorLockMode.Confined;
    }

    public void onPause()
    {
        if (!mainMenu.activeSelf)
        {
            if (!pauseMenu.activeSelf) //Pause
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);

                Cursor.visible = true;
                cursorLock = CursorLockMode.Confined;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else //UnPause
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);

                Cursor.visible = false;
                cursorLock = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void onDestroy()
    {
        cursorLock = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void onDeath()
    {
        ui.SetActive(false);
        creditsOBJ.SetActive(true);
    }
}
