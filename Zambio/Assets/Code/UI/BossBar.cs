using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossBar : MonoBehaviour {

    public GameObject bossBar;
    public RectTransform bossBarOBJ;
    public RectTransform healthBarOBJ;
    public RectTransform healthBarPrevOBJ;
    public Text bossNameOBJ;
    public string bossNameTXT;
    public float healthBase;

    private float healthCur = 0;
    private float healthPrev = 0;

    void Start ()
    {
        //Debug Code
        InvokeRepeating("summonBoss", 0.5f, 0.01f);
    }
	
	void Update ()
    {     
        //Debug Code
        if (Input.GetKeyUp("i"))
        {
            setBossInfo(100);
        }
        if (Input.GetKeyUp("o"))
        {
            setBossInfo(-100);
        }
        if (Input.GetKeyUp("k"))
        {
            setBossInfo(500);
        }
        if (Input.GetKeyUp("l"))
        {
            setBossInfo(-500);
        }
    }

    public void setBossName()
    {
        bossNameOBJ.text = "Boss: " + bossNameTXT;
    }

    public void setBossInfo (int damage)
    {
        if (IsInvoking("setSubBar")) //Stop Regen On Hit
        {
            CancelInvoke("setSubBar");
            healthPrev = healthCur;
            healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
        }

        healthCur -= damage; //Applies Damage

        if (healthCur >= healthBase) //Prevent Health OverFlow
        {
            healthCur = healthBase;
            healthPrev = healthCur;
            healthBarOBJ.localScale = new Vector3(1, 1, 1);
            healthBarPrevOBJ.localScale = new Vector3(1, 1, 1);
        }
        else if (healthCur <= 0) //Boss Defeated
        {
            healthCur = 0;
            healthPrev = 0;
            healthBarPrevOBJ.localScale = new Vector3(0, 1, 1);
            healthBarOBJ.localScale = new Vector3(0, 1, 1);
            //Invoke Kill Boss
            InvokeRepeating("bossDeath", 0.5f, 0.1f);
        }

        if (healthCur < healthPrev) //Boss Damaged Not Defeated
        {
            healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
            healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
            InvokeRepeating("setSubBar", 3, 0.01f);
        }
        else //Fail Safe
        {
            healthPrev = healthCur;
            healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
            healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
        }

    }

    private void setSubBar ()
    {
        if(healthPrev > healthCur) //Boss Health Regen - About 33% of Damage Taken
        {
            healthCur++;
            healthPrev -= 2;
            healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
            healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
        }
        else //Ends Regen and Fix Desync
        {
            healthPrev = healthCur;
            healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
            CancelInvoke("setSubBar");
        }
    }

    private void summonBoss()
    {
        if(healthPrev < healthBase)
        {
            healthPrev++;
            healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
        }
        if(healthCur < healthBase && healthPrev >= healthBase * 0.33)
        {
            healthCur++;
            healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
        }
        if(healthCur == healthBase)
        {
            healthCur = healthBase;
            healthPrev = healthCur;
            healthBarPrevOBJ.localScale = new Vector3((float)healthPrev / healthBase, 1, 1);
            healthBarOBJ.localScale = new Vector3((float)healthCur / healthBase, 1, 1);
            CancelInvoke("summonBoss");
        }
    }

    private void bossDeath()
    {
        if(bossBarOBJ.localScale.x > 0.01f)
        {
            bossBarOBJ.localScale = new Vector3(bossBarOBJ.localScale.x - 0.01f, 1, 1);
        }
        else
        { 
            CancelInvoke("bossDeath");
            bossBar.SetActive(false);
        }
    }

}
