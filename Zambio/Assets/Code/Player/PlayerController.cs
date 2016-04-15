using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    HealthPanel UI;
    HealthPanelDisplay hpDisplay;
    // Use this when you want to increase ammo or add Powerups already applied to character
    public int health { get; set; }
    //describes ammo type not ammount
    private int ammo;
    private List<string> myPowerUps;
    GameControllerSingleton gc;

    // Pattern Practice
    public HealthSubject healthModel;

    //private bool dead = false;
    public int score { get; set; }


    public Inventory myInventory
    {
        get; private set;
    }

    void Awake()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        gc = GameControllerSingleton.get();
        health = 20;
        score = 0;

        // Health Model
        healthModel = new HealthSubject();
        healthModel.SetState(health);

    }
    // Use this for initialization
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("HealthPanel").GetComponent<HealthPanel>();
        hpDisplay = GameObject.FindGameObjectWithTag("HealthStatusDisplay").GetComponent<HealthPanelDisplay>();
        ammo = UI.bullet;
        myInventory = GetComponent<Inventory>();
        myPowerUps = new List<string>();

        // Health Observer Registration

    }

    // Update is called once per frame
    void Update()
    {
        ammo = UI.bullet;

        if (Input.GetButtonDown("PreviousAmmo"))
        {
            if (ammo - 1 < 1)
                ammo = 6;
            UI.changeAmmo(--ammo);
            hpDisplay.setTextToAmmoName();
        }
        if (Input.GetButtonDown("NextAmmo"))
        {
            if (ammo + 1 > 5)
                ammo = 0;
            UI.changeAmmo(++ammo);
            hpDisplay.setTextToAmmoName();
        }
        //switch case impossible because of specific button press -Ryan
        //ShortCut Keys
        if (Input.GetKeyDown("1"))
        {
            UI.changeAmmo(1);
            hpDisplay.setTextToAmmoName();
        }
        if (Input.GetKeyDown("2"))
        {
            UI.changeAmmo(2);
            hpDisplay.setTextToAmmoName();
        }
        if (Input.GetKeyDown("3"))
        {
            UI.changeAmmo(3);
            hpDisplay.setTextToAmmoName();
        }
        if (Input.GetKeyDown("4"))
        {
            UI.changeAmmo(4);
            hpDisplay.setTextToAmmoName();
        }
        if (Input.GetKeyDown("5"))
        {
            UI.changeAmmo(5);
            hpDisplay.setTextToAmmoName();
        }

    }
    void OnTriggerEnter(Collider tColl)
    {
        if (tColl.CompareTag("PickUp"))
        {
            PowerUp thisPowerUp = tColl.GetComponent<PowerUp>();
            // Gain Health
            if (health + thisPowerUp.numQtrHearts < 20)
            {
                setHealth(health + thisPowerUp.numQtrHearts);
                //UI.getHealth();
            }
            else if (health + thisPowerUp.numQtrHearts <= 0) // Rancid Mushroom
            {
                setHealth(0);
            }
            else // targetHealth >= 20
            {
                setHealth(20);
            }

            // Check powerup applied, add to to inventory if not, else add to powerup applied
            if (thisPowerUp.isFire || thisPowerUp.isMetal)
            {

                if (myPowerUps.Contains(thisPowerUp.name))
                {
                    myInventory.AddPower(thisPowerUp);
                }
                myPowerUps.Add(thisPowerUp.name);


            }



            tColl.gameObject.SetActive(false);
            Destroy(tColl.gameObject);
        }
    }

    public bool hasPowerUp(string tName)
    {
        return myPowerUps.Contains(tName);
    }

    private void setHealth(int amt)
    {
        health = amt;
        healthModel.SetState(amt);
        healthModel.Notify();
        if (health < 1)
        {
            deathSequence();
            //dead = true;
        }
    }

    public void adjustScore(int amt)
    {
        score += amt;
        UI.getScore();

    }

    public void adjustHealth(int amt)
    {
        //Debug.Log("Adjusting Health by " + amt);
        setHealth(health + amt);

    }
    void deathSequence()
    {
        Debug.Log("GAME OVER");
        //SceneManager.UnloadScene("Level One");
        //SceneManager.UnloadScene("UI");
        SceneManager.LoadScene("Level One");
        gc.init = false;
        UI.init = false;
        setHealth(20);
        Cursor.lockState = CursorLockMode.None;
        Debug.Log("hi");
        //Game Over
    }
}