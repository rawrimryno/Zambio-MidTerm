using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HealthPanel : MonoBehaviour
{

    UIHealthObserver healthObserver;
    GameControllerSingleton gc;
    public bool init = false;

    public Image[] hearts;
    public Sprite[] heartIcons;
    public int health;

    public int score;
    public Text scoreTXT;

    public Image[] ammo;
    public Sprite[] ammoIcons;
    public int bullet;

    public RectTransform[] crossHairs;
    private float coolDownBase;
    private float coolDownCur;

    void Awake()
    {
        healthObserver = new UIHealthObserver();
    }

    void Start()
    {
        changeAmmo(1);
        bullet = 1;
        score = 0;
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

            healthObserver.healthSubject = gc.pc.healthModel;
            gc.pc.healthModel.Attach(healthObserver);
            healthObserver.healthPanel = this;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            
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

}
