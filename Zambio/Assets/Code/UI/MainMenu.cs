using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject ui;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public CursorLockMode cursorLock;

    void Start () {
        Time.timeScale = 0f;

        cursorLock = CursorLockMode.Confined;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
	}
	
	void Update () {
        if (Input.GetButtonDown("Pause"))
        {
            onPause();
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
        pauseMenu.SetActive(false);
        ui.SetActive(false);
        mainMenu.SetActive(true);

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
