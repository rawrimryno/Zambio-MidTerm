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
    public GameObject eventSystem;

    public Image hurtOverlay;
    public Image healOverlay;
    private float opacity;

    public CursorLockMode cursorLock;

    private bool focus = true;
    private bool focusLock = true;

    public Button[] mmBTN;
    public Button[] pmBTN;
    private int mainBTN = -1;
    private int pauseBTN = -1;
    private bool controller = false;
    private bool keyDown = false;

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

        if (Input.GetAxis("Vertical") > 0.5 && mainMenu.activeInHierarchy && !keyDown && controller)        //MainMenu Controller
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            keyDown = true;
            mainBTN--;
            mmButtonSelect();
        }
        else if(Input.GetAxis("Vertical") < -0.5 && mainMenu.activeInHierarchy && !keyDown && controller)   //MainMenu Controller
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            keyDown = true;
            mainBTN++;
            mmButtonSelect();
        }
        else if (Input.GetAxis("Vertical") > 0.5 && pauseMenu.activeInHierarchy && !keyDown && controller)   //PauseMenu Controller
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            keyDown = true;
            pauseBTN--;
            pmButtonSelect();
        }
        else if (Input.GetAxis("Vertical") < -0.5 && pauseMenu.activeInHierarchy && !keyDown && controller)   //PauseMenu Controller
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            keyDown = true;
            pauseBTN++;
            pmButtonSelect();
        }

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0 && controller)                   //Activate KeyBoard
        {
            controller = false;
            if (Time.timeScale == 0)
            {
                Cursor.visible = true;
            }
            mainBTN = -1;
            pauseBTN = -1;
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            //print("keyboard");
        }
        else if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 && !controller)         //Activate Controller
        {
            controller = true;
            Cursor.visible = false;
            //print("controller");
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) //Checks KeyDown
        {
            keyDown = false;
        }

        //Debug Code
        if (Input.GetKeyUp("p"))
        {
            bossBarOBJ.SetActive(true);
        }
        if (Input.GetKeyUp("t"))
        {
            onDeath();
            creditsOBJ.GetComponent<Credits>().setStart();
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

        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
        mainBTN = -1;

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

    public void mmButtonSelect()
    {
        if(mainBTN > 1)
        {
            mainBTN = 1;
        }
        else if(mainBTN < 0)
        {
            mainBTN = 0;
        }

        mmBTN[mainBTN].Select();
    }

    public void pmButtonSelect()
    {
        if (pauseBTN > 2)
        {
            pauseBTN = 2;
        }
        else if (pauseBTN < 0)
        {
            pauseBTN = 0;
        }

        pmBTN[pauseBTN].Select();
    }

    public void hurt()
    {
        if (IsInvoking("hurtAnimation"))
        {
            CancelInvoke("hurtAnimation");
        }
        if (IsInvoking("healAnimation"))
        {
            CancelInvoke("healAnimation");
            Color c = Color.white;
            c.a = 0;
            healOverlay.color = c;
        }
        opacity = 85;
        InvokeRepeating("hurtAnimation", 0, 0.05f);
    }

    public void hurtAnimation()
    {
        opacity--;
        if(opacity >= 0)
        {
            Color c = hurtOverlay.color;
            c.a = opacity / 100;
            hurtOverlay.color = c;
        }
        else
        {
            CancelInvoke("hurtAnimation");
        }
        //print(hurtOverlay.color);
    }

    public void heal()
    {
        if (IsInvoking("healAnimation"))
        {
            CancelInvoke("healAnimation");
        }
        if (IsInvoking("hurtAnimation"))
        {
            CancelInvoke("hurtAnimation");
            Color c = Color.white;
            c.a = 0;
            hurtOverlay.color = c;
        }
        opacity = 85;
        InvokeRepeating("healAnimation", 0, 0.05f);
    }

    public void healAnimation()
    {
        opacity--;
        if (opacity >= 0)
        {
            Color c = healOverlay.color;
            c.a = opacity / 100;
            healOverlay.color = c;
        }
        else
        {
            CancelInvoke("healAnimation");
        }
        //print(healOverlay.color);
    }

}
