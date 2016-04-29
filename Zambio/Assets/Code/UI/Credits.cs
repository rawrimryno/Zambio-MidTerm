using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

    public GameObject credits;
    public GameObject score;
    public Text scoreOBJ;

    public bool start = false;
    private bool begin = true;
    private bool end = false;
    private bool scoreSET = false;
    private string scoreTXT = "Score:";

    GameControllerSingleton gc;
    UIAmmoObserver ammoObserver;
    UIHealthObserver healthObserver;

    void Start () {
        gc = GameControllerSingleton.get();
        ammoObserver = new UIAmmoObserver();
        FindObjectOfType<Inventory>().ammoContents.ammoSubject.Attach(ammoObserver);

        Time.timeScale = 1;
        Cursor.visible = false;
        InvokeRepeating("textScroll", 1.5f, 0.01f);

        if (Input.anyKey)
        {
            begin = false;
        }
    }
	
	void Update () {
        if (!Input.anyKey && !begin) //Makes sure player doesn't accidently skip a section
        {
            begin = true;
        }
        if ((Input.anyKeyDown && end && begin) || (start && !credits.activeInHierarchy) || (start && Input.anyKeyDown))
        {
            gc.pc.deathSequence();
        }
        if ((Input.anyKeyDown && !start) || (!credits.activeInHierarchy && begin && !end && !start))
        {
            if (IsInvoking("textScroll"))
                CancelInvoke("textScroll");
            if (!scoreSET) //Makes sure score only set once
            {
                scoreDisplay();
                scoreSET = true;
            }
            credits.SetActive(false);
            score.SetActive(true);
            begin = false;
            end = true;
        }
	}

    public void textScroll()
    {
        if (credits.transform.position.y < Screen.height / 2 + credits.transform.position.y / 2 + 15)
        {
            credits.transform.Translate(0, (float)Screen.height / 1080.0f, 0);
        }
        else
        {
            CancelInvoke("textScroll");
            scoreDisplay();
            scoreSET = true;
            credits.SetActive(false);
            if(!start)
                score.SetActive(true);
            end = true;
        }
    }

    public void scoreDisplay()
    {
        scoreTXT = "Final Score: " + (gc.pc.score);// + 10 * ammoObserver.ammoSubject.GetState().returnAmmo(0) + 25 * ammoObserver.ammoSubject.GetState().returnAmmo(1) + 50 * ammoObserver.ammoSubject.GetState().returnAmmo(2) + 75 * ammoObserver.ammoSubject.GetState().returnAmmo(3) + 100 * ammoObserver.ammoSubject.GetState().returnAmmo(4));
        //scoreTXT = scoreTXT + "\n" + "Green Shell: " + ammoObserver.ammoSubject.GetState().returnAmmo(0) + " x 10 = " + 10 * ammoObserver.ammoSubject.GetState().returnAmmo(0);
        //scoreTXT = scoreTXT + "\n" + "Red Shell: " + ammoObserver.ammoSubject.GetState().returnAmmo(1) + " x 25 = " + 25 * ammoObserver.ammoSubject.GetState().returnAmmo(1);
        //scoreTXT = scoreTXT + "\n" + "Blue Shell: " + ammoObserver.ammoSubject.GetState().returnAmmo(2) + " x 50 = " + 50 * ammoObserver.ammoSubject.GetState().returnAmmo(2);
        //scoreTXT = scoreTXT + "\n" + "Bullet Bill: " + ammoObserver.ammoSubject.GetState().returnAmmo(3) + " x 75 = " + 75 * ammoObserver.ammoSubject.GetState().returnAmmo(3);
        //scoreTXT = scoreTXT + "\n" + "Red Bullet Bill: " + ammoObserver.ammoSubject.GetState().returnAmmo(4) + " x 100 = " + 100 * ammoObserver.ammoSubject.GetState().returnAmmo(4);
        //scoreTXT = scoreTXT + "\n" + " Base Score: " + gc.pc.score;
        scoreOBJ.text = scoreTXT;
    }

    public void setStart()
    {
        start = true;
    }

}
