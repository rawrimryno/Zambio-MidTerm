using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    GameControllerSingleton gc;
    NavAgentGoToTransform enemyNav;
    Rigidbody rb;
    Animator anim;
    public PlayerController pc;
    Inventory inventory;

    private bool hasLeftSpawner = true;
    private bool init = false;
    private float yDisp = 0;

    public float pipeHeight = 5;

    public bool dying = false;
    public int health;
    public int damage;
    public int value;
    public float hitTime;  // time for one hit
    private float coolDown=0;
    private bool dead;
    private Vector3 freeze;

    void Awake()
    {
        anim = GetComponent<Animator>();
        //Random.seed = (int)Time.realtimeSinceStartup;

        if (damage <= 0)
        {
            damage = 1;
        }
        if (hitTime <= 0)
        {
            hitTime = 2.0f;
        }
        if (health <= 0)
        {
            health = 1;
        }
        if (value <= 0)
        {
            value = 10;
        }
    }

    // Use this for initialization
    void Start()
    {
        //gc = GameControllerSingleton.get();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        enemyNav = GetComponent<NavAgentGoToTransform>();
        rb = GetComponent<Rigidbody>();

        //inventory = gc.pc.myInventory;
        acquirePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!init && Time.timeScale != 0f)
        {
            gc = GameControllerSingleton.get();
            //inventory = FindObjectOfType<Inventory>();
            inventory = gc.pc.myInventory;
            init = true;
            //Debug.Log("Inventory probably didn't set, don't forget to work on it.");
        }
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if (!gc)
        {
            gc = GameControllerSingleton.get();
        }

        if ( health <= 0)
        {
            if (!dead)
            {
                freeze = this.transform.position;
                dead = true;
            }
            else
            {
                this.transform.position = freeze;
            }
        }


    }

    void OnTriggerEnter(Collider oCol)
    {
        if (!init && oCol.CompareTag("Spawner"))
        {
            hasLeftSpawner = false;
            enemyNav.enabled = false;
        }

        if (oCol.CompareTag("Player"))
        {
            hurtPlayer();
            if ( gc.pc.myPowerUps.Contains("fireFlower") )
            {
                gc.pc.myPowerUps.Remove("fireFlower");
                if (gc.pc.myPowerUps.Contains("fireFlower"))
                {
                    Debug.Log("<b>Removed, but still exists<\b>");
                }
            }
        }
    }

    void OnTriggerStay(Collider oCol)
    {
        float randVal = Random.value * Mathf.PI;

        if (!hasLeftSpawner && oCol.CompareTag("Spawner"))
        {
            if (!init && yDisp < pipeHeight)
            {
                transform.position += new Vector3(0, 1, 0);
                yDisp += 1;
            }
            else
            {
                rb.AddForce(Mathf.Cos(randVal), 0, Mathf.Sin(randVal));
            }
        }

        if (oCol.CompareTag("Player"))
        {
            if (coolDown <= 0 && health > 0)
            {
                hurtPlayer();
            }
        }
    }
    void OnTriggerExit(Collider oCol)
    {
        if (oCol.CompareTag("Spawner"))
        {
            hasLeftSpawner = true;
            init = true;
            enemyNav.enabled = true;
            acquirePlayer();
        }
    }

    void acquirePlayer()
    {
        if (enemyNav)
        {
            enemyNav.target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void hurtPlayer()
    {
       // Debug.Log("Hurting Player");
        gc.pc.adjustHealth(-damage);
        coolDown = hitTime;
    }

    public void randomDrop()
    {
        System.Random rand = new System.Random();
        //150 so 2/3 of the time it will drop a a randomdrop;
        int itemType = rand.Next(0, 150);
        bool metal = false;
        GameObject renameMe;
        if (itemType < 50)
        {
            //make ammo
            if (itemType < 25)
            {
                inventory.ammoContents.setAmmo(0, inventory.ammoContents.returnAmmo(0) + 5);
            }
            else if (itemType < 37)
            {
                inventory.ammoContents.setAmmo(1, inventory.ammoContents.returnAmmo(1) + 5);
            }
            else if (itemType < 44)
            {
                inventory.ammoContents.setAmmo(2, inventory.ammoContents.returnAmmo(2) + 5);
            }
            else if (itemType < 48)
            {
                inventory.ammoContents.setAmmo(3, inventory.ammoContents.returnAmmo(3) + 5);
            }
            else if (itemType < 50)
            {
                inventory.ammoContents.setAmmo(4, inventory.ammoContents.returnAmmo(4) + 5);
            }
        }
        else
        {
            PowerUpDesc prefab = new PowerUpDesc();
            //instatiate powerup or mushroom
            if (itemType < 65)
            {
                gc.powerUpByID.TryGetValue(2, out prefab);

            }
            else if (itemType < 78)
            {
                gc.powerUpByID.TryGetValue(0, out prefab);

            }
            else if (itemType < 80)
            {
                gc.powerUpByID.TryGetValue(1, out prefab);

            }
            else if (itemType < 90)
            {
                gc.powerUpByID.TryGetValue(3, out prefab);

            }
            else if (itemType <= 100)
            {
                gc.powerUpByID.TryGetValue(4, out prefab);
                metal = true;

            }
            if (itemType <= 100)
            {
                if (metal == false)
                {
                    renameMe = Instantiate(prefab.prefab, transform.position + new Vector3(0,1,0), new Quaternion(0, 0, 0, 0)) as GameObject;
                    renameMe.name = prefab.prefab.name;
                }
                else
                {
                    renameMe = Instantiate(prefab.prefab, transform.position + new Vector3(0, 3, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
                    renameMe.name = prefab.prefab.name;
                }
            }

        }
    }
}