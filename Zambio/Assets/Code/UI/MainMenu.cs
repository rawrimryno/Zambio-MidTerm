using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject ui;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject bossBarOBJ;

    public CursorLockMode cursorLock;

    private bool focus = true;
    private bool focusLock = true;

    public Button[] mmBTN;
    public Button[] pmBTN;
    private int mainBTN = -1;
    private int pauseBTN = -1;

    GameControllerSingleton gc;

    void Start () {
        Time.timeScale = 0f;

        cursorLock = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        gc = GameControllerSingleton.get();
    }
	
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            onPause();
        }
        if (!focus && focusLock) //Pauses Game on Focus Loss
        {
            onPause();
        }

        //if (Input.GetAxis("Vertical") > 0 && mainMenu.activeInHierarchy)
        //{
        //    mainBTN++;
        //    if (mainBTN > 1)
        //        mainBTN = 0;
        //}
        //if (Input.GetAxis("Vertical") < 0 && mainMenu.activeInHierarchy)
        //{
        //    mainBTN--;
        //    if (mainBTN < 0)
        //        mainBTN = 1;
        //}
        //if (Input.GetButtonDown("Submit") && mainMenu.activeInHierarchy)
        //{
        //    switch (mainBTN)
        //    {
        //        case 0:
        //            onPlay();
        //            break;
        //        case 1:
        //            onQuit();
        //            break;
        //        default:
        //            break;
        //    }
        //    mainBTN = -1;
        //}

        //if (Input.GetAxis("Mouse Y") > 0 && pauseMenu.activeInHierarchy)
        //{
        //    pauseBTN++;
        //    if (pauseBTN > 2)
        //        pauseBTN = 0;
        //    //print("pmBTN: " + pauseBTN);
        //}
        //if (Input.GetAxis("Mouse Y") < 0 && pauseMenu.activeInHierarchy)
        //{
        //    pauseBTN--;
        //    if (pauseBTN < 0)
        //        pauseBTN = 2;
        //    //print("pmBTN: " + pauseBTN);
        //}
        //if (Input.GetButtonDown("Submit") && pauseMenu.activeInHierarchy)
        //{
        //    switch (pauseBTN)
        //    {
        //        case 0:
        //            onMenu();
        //            break;
        //        case 1:
        //            onPause();
        //            break;
        //        case 2:
        //            onQuit();
        //            break;
        //        default:
        //            break;
        //    }
        //    pauseBTN = -1;
        //}

        //Debug Code
        if (Input.GetKeyUp("p"))
        {
            bossBarOBJ.SetActive(true);
        }
	}

    void OnApplicationFocus(bool focusStatus)
    {
        if(Time.timeScale != 0.0f)
        {
            focus = focusStatus;
            if (focus)
                focusLock = false;
            else
                focusLock = true;
        } 
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


}
