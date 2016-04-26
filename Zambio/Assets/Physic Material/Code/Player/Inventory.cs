using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public AmmoContents ammoContents;
    public Dictionary<string, int> powerUpContents { get; private set; }
    //GameControllerSingleton gc;
    //public delegate void ProcessPowerUpDelegate(PowerUp powerUp);


    void Awake()
    {
        ammoContents = new AmmoContents();
        powerUpContents = new Dictionary<string, int>();
    }

    // Use this for initialization
    void Start()
    {
        //gc = GameControllerSingleton.get();
    }

    // Update is called once per frame
    void Update()
    {
        //if ( !observerRegistered)
        //{
        //    if (ammoContents.ammoSubject.Attach(FindObjectOfType<PlayerShoot>().ammoObserver))
        //    {
        //        observerRegistered = true;
        //    }
        //}
        // Who broadcasts?
    }

    // Add a Power Up into inventory
    // Precondition:  Must already have powerUp applied to player and a PowerUp
    // PostCondition:  count-value associated with name-key will be incremented by one
    public void AddPower(PowerUp powerUp)
    {
        int count = 0;
        if (powerUpContents.TryGetValue(powerUp.name, out count))
        {
            powerUpContents.Remove(powerUp.name);
            count++;
        }
        powerUpContents.Add(powerUp.name, count);
    }
}
[Serializable]
public class AmmoContents 
{

    public enum ammoTypes { GREEN, RED, BLUE, BULLET, REDBULLET }
    public int greenShells { get; private set; }
    public int redShells { get; private set; }
    public int blueShells { get; private set; }
    public int bulletBill { get; private set; }
    public int redBulletBill { get; private set; }
    public AmmoSubject ammoSubject = new AmmoSubject();

    public int returnAmmo(int ammoNum)
    {
        switch (ammoNum)
        {
            case 0:
                return greenShells;
            case 1:
                return redShells;
            case 2:
                return blueShells;
            case 3:
                return bulletBill;
            case 4:
                return redBulletBill;
            default:
                return 0;
        }
    }

    public AmmoContents()
    {
        //ammoSubject.Attach(FindObjectOfType<PlayerShoot>().ammoObserver);
        greenShells = 30;
        redShells = 10;
        blueShells = 5;
        bulletBill = redBulletBill = 5;
        ammoSubject.SetState(this);
        ammoSubject.Notify();

    }

    // For System Wide Use
    // Ammo myAmmo = inventory.ammoContents;
    // int numGreenShells = myAmmo.greenShells;
    // setAmmo( ammoTypes.GREEN, --numGreenShells);

    public void setAmmo(int type, int num)
    {
        switch (type)
        {
            case 0:
                greenShells = num; break;
            case 1:
                redShells = num; break;
            case 2:
                blueShells = num; break;
            case 3:
                bulletBill = num; break;
            case 4:
                redBulletBill = num; break;
            default: break;
        }
        ammoSubject.SetState(this);
        ammoSubject.Notify();
    }
}

// I think this is the flyweight pattern
// Loaded by GameController
public class AmmoDesc
{
    public int ID { get; private set; }
    public string desc { get; private set; }
    public string sName { get; private set; }
    public string dName { get; private set; }
    public Sprite sprite { get; private set; }
    public GameObject prefab { get; private set; }
    GameControllerSingleton gc = GameControllerSingleton.get();

    public bool setID(int id)
    {
        ID = id;
        return true;
    }
    public bool setSName(string stringName)
    {
        sName = stringName;
        return true;
    }
    public bool setDName(string displayName)
    {
        dName = displayName;
        return true;
    }
    public bool setDesc(string description)
    {
        desc = description;
        return true;
    }
    public bool setSprite(int id, Sprite ammoSprite)
    {
        bool result = false;
        AmmoDesc thisAmmoDesc = new AmmoDesc(); 
        if (gc.ammoByID.TryGetValue(id, out thisAmmoDesc))
        {
            result = true;
            thisAmmoDesc.sprite = ammoSprite;
            gc.ammoByID.Remove(id);
            gc.ammoByID.Add(id, thisAmmoDesc);
        }

        return result;
    }

    public bool setPrefab(int id, GameObject inPrefab)
    {
        bool result = false;
        AmmoDesc ammoDesc = new AmmoDesc();
        if (gc.ammoByID.TryGetValue(id, out ammoDesc))
        {
            result = true;
            ammoDesc.prefab = inPrefab;
            gc.ammoByID.Remove(id);
            gc.ammoByID.Add(id, ammoDesc);
        }

        return result;
    }

    public AmmoDesc()
    {
        sprite = null;
    }


}
