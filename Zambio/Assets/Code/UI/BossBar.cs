using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossBar : MonoBehaviour
{

    //public GameObject bossBar;
    //public RectTransform bossBarOBJ;
    public RectTransform healthBarOBJ;
    //public RectTransform healthBarPrevOBJ;
    //public Text bossNameOBJ;
    //public string bossNameTXT;
    GameControllerSingleton gc;

    private int healthBase = 0;
    private int healthCur = 0;
    private int healthPrev = 0;
    private int smoothHp = 0;

    private EnemyController ec;
    private Bowser bs;
    private bool summoned = false;

    void Start()
    {
        summoned = false;
        gc = GameControllerSingleton.get();
        bs = GameObject.Find("Bowser").GetComponent<Bowser>();
    }

    void Update()
    {

        //print("GameObject: " + GameObject.Find("Bowser").activeInHierarchy != null);
        //print("Summoned: " + summoned);

        if (gc.hasBossSpawned() && !summoned) //On Summoned
        {
            Debug.Log("Logging Birth");
            ec = GameObject.Find("Bowser").GetComponent<EnemyController>();
            healthBase = ec.health;
            healthCur = healthBase;
            Debug.Log("CurHealth - " + healthCur);
            Debug.Log("BaseHealth - " + healthBase);
            //InvokeRepeating("summonBoss", 0, 0.01f);

            healthBarOBJ.localScale = new Vector3(1, 1, 1);
            healthBarOBJ.gameObject.transform.localScale = new Vector3(1, 1, 1);
            summoned = true;
        }

        if (gc.isBossDead() && summoned) //On Death
        {
            Debug.Log("Logging Death");
            healthBarOBJ.localScale = new Vector3(0, 0, 0);
            GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0); //Zach Edit
            summoned = false;
            //if (IsInvoking("smoothHealth"))
            //    CancelInvoke("smoothHealth");
            //if (IsInvoking("summonBoss"))
            //    CancelInvoke("summonBoss");
            healthBase = 0;
            healthCur = 0;
            smoothHp = 0;

        }

        if (ec != null && ec.health != healthCur && healthCur > 0)
        {
            //Summoned & On Hit & Not Dead
            //{
            //    if (IsInvoking("smoothHealth"))
            //    {
            //        CancelInvoke("smoothHealth");
            //    }
            //    else if (IsInvoking("summonBoss"))
            //    {
            //        CancelInvoke("summonBoss");
            //    }
            float percentHealth = (float)healthCur / healthBase;
            Debug.Log("Percent Health - " + percentHealth);
            healthBarOBJ.localScale = new Vector3(percentHealth, 1, 1);
            healthCur = ec.health;
            if (healthCur > 0)
            {
                //smoothHp = healthCur * 100;
                //InvokeRepeating("smoothHealth", 0, 0.01f);
            }
            else
            {
                healthBarOBJ.localScale = new Vector3(0, 0, 0);
            }
        }
    }
}

        //Debug Code
        //if (Input.GetKeyUp("i"))
        //{
        //    setBossInfo(1);
        //}
        //if (Input.GetKeyUp("o"))
        //{
        //    setBossInfo(-1);
        //}
        //if (Input.GetKeyUp("k"))
        //{
        //    setBossInfo(5);
        //}
        //if (Input.GetKeyUp("l"))
        //{
        //    setBossInfo(-5);
        //}
    
    //public void smoothHealth()
    //{
    //    if(smoothHp > (ec.health * 100))
    //    {
    //        smoothHp--;
    //        healthBarOBJ.localScale = new Vector3((float)smoothHp / (healthBase * 100), 1, 1);
    //    }
    //    else
    //    {
    //        CancelInvoke("smoothHealth");
    //    }
    //}

    //public void summonBoss()
    //{
    //    if (smoothHp < (healthBase * 100))
    //    {
    //        smoothHp++;
    //        healthBarOBJ.localScale = new Vector3((float)smoothHp / (healthBase * 100), 1, 1);
    //    }
    //    else
    //    {
    //        CancelInvoke("summonBoss");
    //    }
    //}

    //public void setBossName()
    //{
    //    bossNameOBJ.text = "Boss: " + bossNameTXT;
    //}

    //public void setBossInfo (int damage)
    //{
    //    if (IsInvoking("setSubBar")) //Stop Regen On Hit
    //    {
    //        CancelInvoke("setSubBar");
    //        healthPrev = healthCur;
    //        healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
    //    }

    //    healthCur -= damage; //Applies Damage

    //    if (healthCur >= healthBase) //Prevent Health OverFlow
    //    {
    //        healthCur = healthBase;
    //        healthPrev = healthCur;
    //        healthBarOBJ.localScale = new Vector3(1, 1, 1);
    //        healthBarPrevOBJ.localScale = new Vector3(1, 1, 1);
    //    }
    //    else if (healthCur <= 0) //Boss Defeated
    //    {
    //        healthCur = 0;
    //        healthPrev = 0;
    //        healthBarPrevOBJ.localScale = new Vector3(0, 1, 1);
    //        healthBarOBJ.localScale = new Vector3(0, 1, 1);
    //        //Invoke Kill Boss
    //        InvokeRepeating("bossDeath", 0.5f, 0.1f);
    //    }

    //     else if (healthCur < healthPrev) //Boss Damaged Not Defeated
    //    {
    //        healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
    //        healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
    //        InvokeRepeating("setSubBar", 3, 0.01f);
    //    }
    //    else //Fail Safe
    //    {
    //        healthPrev = healthCur;
    //        healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
    //        healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
    //    }

    //}

    //private void setSubBar ()
    //{
    //    if(healthPrev > healthCur) //Boss Health Regen - About 33% of Damage Taken
    //    {
    //        healthCur++;
    //        healthPrev -= 2;
    //        healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
    //        healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
    //    }
    //    else //Ends Regen and Fix Desync
    //    {
    //        healthPrev = healthCur;
    //        healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
    //        CancelInvoke("setSubBar");
    //    }
    //}

    //private void summonBoss()
    //{
    //    if(healthPrev < healthBase)
    //    {
    //        healthPrev++;
    //        healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
    //    }
    //    if(healthCur < healthBase && healthPrev >= healthBase * 0.33)
    //    {
    //        healthCur++;
    //        healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
    //    }
    //    if(healthCur == healthBase)
    //    {
    //        healthCur = healthBase;
    //        healthPrev = healthCur;
    //        healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
    //        healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
    //        CancelInvoke("summonBoss");
    //    }
    //}

    //private void bossDeath()
    //{
    //    if(bossBarOBJ.localScale.x > 0.01f)
    //    {
    //        bossBarOBJ.localScale = new Vector3(bossBarOBJ.localScale.x - 0.01f, 1, 1);
    //    }
    //    else
    //    { 
    //        CancelInvoke("bossDeath");
    //        bossBar.GetComponent<RectTransform>().localScale = new Vector3(0, 0, 0);
    //    }
    //}
    