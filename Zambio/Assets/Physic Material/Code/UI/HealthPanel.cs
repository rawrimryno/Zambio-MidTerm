using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HealthPanel : MonoBehaviour
{

    UIHealthObserver healthObserver;
    UIAmmoObserver ammoObserver;
    StateMachine sm;
    GameControllerSingleton gc;
    public bool init = false;

    public Image[] hearts;
    public Sprite[] heartIcons;
    public int health;

    public int score;
    public Text scoreTXT;

    public int round;
    public Text roundTXT;

    public Image[] ammo;
    public Sprite[] ammoIcons;
    public Text ammoTXT;
    public int bullet;
    private int bulletCount;

    public RectTransform[] crossHairs;
    //private float coolDownBase = 5;
    private float coolDownCur;

    void Awake()
    {
        healthObserver = new UIHealthObserver();
        ammoObserver = new UIAmmoObserver();
        sm = GetComponentInParent<StateMachine>();
    }

    void Start()
    {
        changeAmmo(1);
        bullet = 1;
        score = 0;
        round = 0;
        scoreTXT = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        gc = GameControllerSingleton.get();
    }

    void Update()
    {
        
        if (!init)
        {
            scoreTXT = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
            gc = GameControllerSingleton.get();
            init = true;

            FindObjectOfType<Inventory>().ammoContents.ammoSubject.Attach(ammoObserver);
            sm = FindObjectOfType<StateMachine>(); //GetComponentInParent<StateMachine>();

            healthObserver.healthSubject = gc.pc.healthModel;
            gc.pc.healthModel.Attach(healthObserver);
            healthObserver.healthPanel = this;
        }

        if(bulletCount != ammoObserver.ammoSubject.GetState().returnAmmo(bullet - 1)) //Check for ammo pickup or switch ammo type
        {
            bulletCount = ammoObserver.ammoSubject.GetState().returnAmmo(bullet - 1);
            ammoCount(bulletCount);
        }

        if ((Input.GetButtonDown("Fire1") || (Input.GetAxis("XboxTriggers") == 1)) && ammoObserver.ammoSubject.GetState().returnAmmo(bullet - 1) > 0 && Time.timeScale == 1 && !IsInvoking("coolDownBar")) //CoolDown Animation
        {
            //coolDown(coolDownBase);
        }

        if(round != sm.Round) //Checks and Updates Round Number if needed
        {
            round = sm.Round;
            roundTXT.text = "Round " + round;
        }

    }

    public void getScore()
    {
        score = gc.pc.score;
        setScore();
    }

    public void setScore()
    {
        if (scoreTXT)
        {
            scoreTXT.text = "Score: " + score;
        }
    }

    public void getHealth()
    {
        setHearts();
    }

    public void setHearts(int health)
    {
        if (health < 0)
        {
            health = 0;
        }
        if (health > 20)
        {
            health = 20;
        }

        int fullHeart = health / 4;
        int partHeart = health % 4;

        for (int i = 0; i < 5; i++)
        {
            if (i <= fullHeart - 1)
            {
                hearts[i].sprite = heartIcons[3];
                hearts[i].color = Color.red;
            }
            else if (i == fullHeart && partHeart > 0)
            {
                switch (partHeart)
                {
                    case 1:
                        hearts[i].sprite = heartIcons[0];
                        break;
                    case 2:
                        hearts[i].sprite = heartIcons[1];
                        break;
                    case 3:
                        hearts[i].sprite = heartIcons[2];
                        break;
                    default:
                        break;
                }
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.clear;
            }
        }
    }

    public void setHearts()
    {

        if (health < 0)
        {
            health = 0;
        }
        if (health > 20)
        {
            health = 20;
        }

        int fullHeart = health / 4;
        int partHeart = health % 4;

        for (int i = 0; i < 5; i++)
        {
            if (i <= fullHeart - 1)
            {
                hearts[i].sprite = heartIcons[3];
                hearts[i].color = Color.red;
            }
            else if (i == fullHeart && partHeart > 0)
            {
                switch (partHeart)
                {
                    case 1:
                        hearts[i].sprite = heartIcons[0];
                        break;
                    case 2:
                        hearts[i].sprite = heartIcons[1];
                        break;
                    case 3:
                        hearts[i].sprite = heartIcons[2];
                        break;
                    default:
                        break;
                }
                hearts[i].color = Color.red;
            }
            else
            {
                hearts[i].color = Color.clear;
            }
        }

    }

    public void changeAmmo(int ammoType)
    {

        bullet = ammoType;

        if (bullet < 1)
        {
            bullet = 5;
        }
        if (bullet > 5)
        {
            bullet = 1;
        }

        switch (bullet)
        {
            case 1:
                ammo[0].sprite = ammoIcons[4];
                ammo[1].sprite = ammoIcons[3];
                ammo[2].sprite = ammoIcons[2];
                ammo[3].sprite = ammoIcons[1];
                ammo[4].sprite = ammoIcons[0];
                break;
            case 2:
                ammo[0].sprite = ammoIcons[3];
                ammo[1].sprite = ammoIcons[2];
                ammo[2].sprite = ammoIcons[1];
                ammo[3].sprite = ammoIcons[0];
                ammo[4].sprite = ammoIcons[4];
                break;
            case 3:
                ammo[0].sprite = ammoIcons[2];
                ammo[1].sprite = ammoIcons[1];
                ammo[2].sprite = ammoIcons[0];
                ammo[3].sprite = ammoIcons[4];
                ammo[4].sprite = ammoIcons[3];
                break;
            case 4:
                ammo[0].sprite = ammoIcons[1];
                ammo[1].sprite = ammoIcons[0];
                ammo[2].sprite = ammoIcons[4];
                ammo[3].sprite = ammoIcons[3];
                ammo[4].sprite = ammoIcons[2];
                break;
            case 5:
                ammo[0].sprite = ammoIcons[0];
                ammo[1].sprite = ammoIcons[4];
                ammo[2].sprite = ammoIcons[3];
                ammo[3].sprite = ammoIcons[2];
                ammo[4].sprite = ammoIcons[1];
                break;
            default:
                break;
        }

    }

    public void ammoCount(int amount)
    {
        if(amount == 0)
        {
            ammoTXT.text = "0";
            ammoTXT.color = Color.red;
        }
        else if(amount > 99)
        {
            ammoTXT.text = "99";
            ammoTXT.color = Color.cyan;
        }
        else
        {
            ammoTXT.text = amount.ToString();
            ammoTXT.color = Color.green;
        }
    }

    //public void coolDown(float length)
    //{
    //    crossHairs[0].Translate(-6, 6, 0);
    //    crossHairs[1].Translate(6, 6, 0);
    //    crossHairs[2].Translate(-6, -6, 0);
    //    crossHairs[3].Translate(6, -6, 0);
    //    coolDownCur = 100;
    //    InvokeRepeating("coolDownBar", 0, length / 100);
    //}

    //private void coolDownBar()
    //{
    //    if(coolDownCur > 0)
    //    {
    //        crossHairs[0].Translate(0.06f, -0.06f, 0);
    //        crossHairs[1].Translate(-0.06f, -0.06f, 0);
    //        crossHairs[2].Translate(0.06f, 0.06f, 0);
    //        crossHairs[3].Translate(-0.06f, 0.06f, 0);
    //        //print("coolDown");
    //    }
    //    else
    //    {
    //        //print("end coolDown");
    //        CancelInvoke("coolDownBar");
    //    }
    //    coolDownCur--;
    //}

}
