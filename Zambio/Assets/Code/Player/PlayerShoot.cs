using UnityEngine;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour {

    public GameObject[] ammo;
    public GameObject fireAmmo;
    public float bulletSpeed;
    public float rateOfFire;
    public UIAmmoObserver ammoObserver;
    private float timer;
    private float initRoF;
    private Rigidbody projectile;
    private int ammoNum;
    private HealthPanel UI;
    //private HealthPanelDisplay hpDisplay;
    private GameControllerSingleton gc;
    private bool ammoInit = false;
    private bool observerRegistered = false;

	// Use this for initialization
	void Start () {
        UI = GameObject.FindGameObjectWithTag("HealthPanel").GetComponent<HealthPanel>();
        //hpDisplay = GameObject.FindGameObjectWithTag("HealthStatusDisplay").GetComponent<HealthPanelDisplay>();
        ammoNum = UI.bullet;
        if (bulletSpeed == 0)
        {
            bulletSpeed = 20.0f;
        }
        if (rateOfFire == 0)
        {
            rateOfFire = 1.0f;
        }
        
        initRoF = timer = rateOfFire;
        projectile = ammo[0].GetComponent<Rigidbody>();
        gc = GameControllerSingleton.get();
        ammoObserver = new UIAmmoObserver();
	}
	
	// Update is called once per frame
	void Update () {

        if ( !observerRegistered)
        {
            if (FindObjectOfType<Inventory>().ammoContents.ammoSubject.Attach(ammoObserver))
            {
                observerRegistered = true;
            }
        }

        if ( !ammoInit)
        {
            int i = 0;
            AmmoDesc tempAmmoDesc;
            while (i < gc.numAmmo)
            {
                if (gc.ammoByID.TryGetValue(i, out tempAmmoDesc))
                {
                    ammo[i] = tempAmmoDesc.prefab;
                    i++;
                }
            }
            ammoInit = true;
        }

        ammoNum = UI.bullet-1;
        if ((Input.GetButtonDown("Fire1") || (Input.GetAxis("XboxTriggers") == 1) ) && rateOfFire <= 0 && Time.timeScale != 0f && ammoObserver.ammoSubject.GetState().returnAmmo(ammoNum) > 0)
        {
            Rigidbody clone;
            GameObject fireParent;
            //Homing thisHomer;
            if (gc.pc.myPowerUps.Contains("fireFlower"))
            {
                fireParent = Instantiate(fireAmmo, transform.position, Quaternion.identity) as GameObject;
                projectile = ammo[ammoNum].GetComponent<Rigidbody>();
                clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody;
                clone.transform.parent = fireParent.transform;
                fireParent.GetComponent<Homing>().target = clone.transform;
                //var col = fireParent.GetComponentInChildren<ParticleSystem>().subEmitters.birth0.colorOverLifetime;
                //col.enabled = true;
                //col.color = new ParticleSystem.MinMaxGradient(fireParent.GetComponentInChildren<FireColorChanger>().getGrad(ammoNum+1));
            }
            else {
                projectile = ammo[ammoNum].GetComponent<Rigidbody>();
                clone = Instantiate(projectile, (transform.position), transform.rotation) as Rigidbody;
            }
            clone.velocity = transform.TransformDirection((Vector3.forward) * bulletSpeed);
            clone.name = projectile.name;
            switch (ammoNum)
            {
                case 0:
                    timer = initRoF;
                    break;
                case 1:
                    timer = initRoF * 3f;
                    break;
                case 2:
                    timer = initRoF * 4f;
                    break;
                case 3:
                    timer = initRoF * 5f;
                    break;
                case 4:
                    timer = initRoF * 7f;
                    break;
                default:
                    timer = initRoF;
                    break;
            }
            ammoObserver.ammoSubject.GetState().setAmmo(ammoNum, ammoObserver.ammoSubject.GetState().returnAmmo(ammoNum)-1);
            //Debug.Log(ammo[ammoNum].name + " has " + ammoObserver.ammoSubject.GetState().returnAmmo(ammoNum) + " ammo.");
            rateOfFire = timer;
        }
        else
        {
            rateOfFire -= Time.deltaTime;
        }
	}
}
