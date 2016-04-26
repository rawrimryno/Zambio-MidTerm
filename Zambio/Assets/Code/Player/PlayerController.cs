using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    HealthPanel UI;
    HealthPanelDisplay hpDisplay;
    MainMenu MM; //Zach Edit
    int ammo;//describes ammo type not ammount
    GameControllerSingleton gc;
    GameObject regMario;
    GameObject metalMario;
    AudioSource audioSource;
    BossObserver bossObserver;
    bool playedBowserDead = false;


    // Use this when you want to increase ammo or add Powerups already applied to character
    public int health { get; set; }
    public int metalHealth;
    public int initMetalHealth;
    public List<string> myPowerUps;
    public bool isMetalMario;
    public bool impervious;

    public AudioClip[] audioClips;

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
        audioSource = GetComponent<AudioSource>();
        health = 20;
        score = 0;

        // Health Model
        healthModel = new HealthSubject();
        healthModel.SetState(health);
        impervious = false;

    }
    // Use this for initialization
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("HealthPanel").GetComponent<HealthPanel>();
        MM = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>(); //Zach Edit
        hpDisplay = GameObject.FindGameObjectWithTag("HealthStatusDisplay").GetComponent<HealthPanelDisplay>();
        ammo = UI.bullet;
        myInventory = GetComponent<Inventory>();
        regMario = GameObject.Find("Mario") ;
        metalMario = GameObject.Find("MetalMario");

        myPowerUps = new List<string>();

        if (metalHealth == 0)
        {
            metalHealth = 20;
        }

        initMetalHealth = metalHealth;
        isMetalMario = false;
        // Health Observer Registration
        audioSource.clip = audioClips[0];
        audioSource.Play(); 
    }

    // Update is called once per frame
    void Update()
    {

        // Boss Observer For Audio Timing - Todd
        if ( bossObserver == null && gc.hasBossSpawned())
        {
            bossObserver = new BossObserver();
            FindObjectOfType<Bowser>().AddObserver(bossObserver);
            Debug.Log("Bowser is being observed by the PlayerController");
        }

        // Have we played the audio for bowser death? - Todd
        if ( !playedBowserDead && gc.isBossDead())
        {
            audioSource.Stop();
            audioSource.clip = audioClips[4];
            audioSource.Play();

            playedBowserDead = true;
        }

        //Metal Mario Checks-Ryan

        if (isMetalMario)
        {
            regMario.SetActive(false);
            metalMario.SetActive(true);
        }
        else
        {
            regMario.SetActive(true);
            metalMario.SetActive(false);
        }

        //Ammo-Ryan

        ammo = UI.bullet;

        if (Input.GetButtonDown("PreviousAmmo") || Input.GetAxis("Mouse ScrollWheel") > 0 && Time.timeScale != 0)
        {
            if (ammo - 1 < 1)
                ammo = 6;
            UI.changeAmmo(--ammo);
            hpDisplay.setTextToAmmoName();
        }
        if (Input.GetButtonDown("NextAmmo") || Input.GetAxis("Mouse ScrollWheel") < 0 && Time.timeScale != 0)
        {
            if (ammo + 1 > 5)
                ammo = 0;
            UI.changeAmmo(++ammo);
            hpDisplay.setTextToAmmoName();
        }
        //switch case impossible because of specific button press -Ryan
        //ShortCut Keys
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GetComponentInChildren<PlayerShoot>().ammoObserver.ammoSubject.GetState().setAmmo(4, 999);
            Debug.Log("Stupid Check");
        }
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
                else {
                    if (thisPowerUp.isMetal)
                    {
                        isMetalMario = true;
                    }
                    myPowerUps.Add(thisPowerUp.name);
                }
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
            impervious = true;
            MM.onDeath(); //Zach Edit
            //deathSequence(); //Zach Edit
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
        if (amt < 0 && !impervious)
        {
            if (myPowerUps.Contains("metalMario"))
            {
                // Stop current Effect, Load Damage Sound, Play it
                // This is for metal damage taking.  - Todd
                audioSource.Stop();
                audioSource.clip = audioClips[5];
                audioSource.Play();
                isMetalMario = true;
                if (amt == -1)
                {
                    metalHealth -= 1;
                }
                else
                {
                    amt /= 2;
                    metalHealth += amt;
                    setHealth(health + amt);
                }
                if (metalHealth <= 0)
                {
                    myPowerUps.Remove("metalMario");
                    metalHealth = initMetalHealth;
                }
                //Debug.Log("Metal Mario Health: " + metalHealth);
                Debug.Log("Damage Taken: " + amt);
            }
            else
            {
                isMetalMario = false;
                setHealth(health + amt);
                // Stop current Effect, Load Damage Sound, Play it
                // This is for non-metal damage taking.  - Todd
                audioSource.Stop();
                audioSource.clip = audioClips[3];
                audioSource.Play();
            }
        }
        else
        {
            setHealth(health + amt);
        }

    }
    public void deathSequence()
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