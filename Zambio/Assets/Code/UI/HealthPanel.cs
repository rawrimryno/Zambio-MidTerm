using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class HealthPanel : MonoBehaviour
{

    public RectTransform coolDownBar;
    public float coolDownBase;
    public Image[] hearts;
    public Sprite[] heartIcons;
    public int health;
    public int score;
    public Text scoreTXT;
    public Image[] ammo;


    // For Heart Change
    GameControllerSingleton gc;


    public Sprite[] ammoIcons;

    public int bullet;

    private float coolDownCur;

    public bool init = false;

    void Start()
    {
        coolDownCur = coolDownBase;
        changeAmmo(1);
        coolDownCur = 0.5f; // Zach's original
        bullet = 1;
        score = 0;

        gc = GameControllerSingleton.get();
        //ammoIcons = new Sprite[gc.numAmmo];

        //if (!init)
        //{
        //    int i = 0;
        //    Sprite tempSpr;
        //    while (i < gc.numAmmo)
        //    {
        //        tempSpr = gc.getAmmoSpriteByID(i);
        //        ammo[i].sprite = tempSpr;
        //        //ammo[i].sprite = ammoIcons[0];
        //        i++;
        //    }
        //    init = true;

        //}


    }

    void Update()
    {
        if (!init)
        {
            gc = GameControllerSingleton.get();
            init = true;
            Debug.Log("got GameController in health panel");
        }
        //int i = 0;
        //if (!init)
        //{
        //    Sprite tempSpr;
        //    while (i < gc.numAmmo)
        //    {
        //        tempSpr = gc.getAmmoSpriteByID(i);
        //        ammoIcons[i++] = tempSpr;
        //    }
        //    init = true;

        //}

        if (Input.GetButtonDown("Fire1"))
        {
            coolDown();
        }

        //Debug Code 
        if (Input.GetKeyDown("z"))
        {
            health--;
            setHearts();
        }
        if (Input.GetKeyDown("x"))
        {
            health++;
            setHearts();
        }
        getScore();
        getHealth();
        
    }

    public void coolDown()
    {

        if (coolDownCur == coolDownBase)
        {
            coolDownCur = 0f;
            coolDownBar.localScale = new Vector3(0, 0, 1);
            InvokeRepeating("coolDownScaler", 0, 0.01f);
        }

    }

    private void coolDownScaler() //Visual Aid
    {
        if (coolDownCur < coolDownBase)
        {
            coolDownCur++;
            coolDownBar.localScale = new Vector3((float)coolDownCur / coolDownBase, (float)coolDownCur / coolDownBase, 1);
        }
        else
        {
            CancelInvoke("coolDownScaler");
        }
    }

    public void getScore()
    {
        score = gc.pc.score;
        setScore();
        //Debug.Log("ScorePanel is Getting Score " + score);
    }

    public void setScore()
    {
        scoreTXT.text = "Score: " + score;
    }

    public void getHealth()
    {
        health = gc.pc.health;
        setHearts();
        //Debug.Log("HealthPanel is Getting Health " + health);
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
        //Sprite test = gc.getAmmoSpriteByID(0);
        //if (!IsInvoking("coolDownScaler")) //Remove To Have A Blast
        {

            coolDownCur = coolDownBase;
            coolDownBar.localScale = new Vector3((float)coolDownCur / coolDownBase, (float)coolDownCur / coolDownBase, 1);

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

}
