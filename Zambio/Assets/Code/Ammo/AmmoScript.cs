using UnityEngine;
using System.Collections;
using System;

public class AmmoScript : MonoBehaviour
{
    //private Rigidbody rb;
    public int hitCounts;
    public int damage;
    private int hitsLeft;
    private int ID;
    public float speed;
    bool init = false;
    private Inventory inventory;


    public float spinFactor;

    NavMeshAgent meshAgent;
    NavAgentGoToTransform navAgent;

    public float age=0, lifetime=4;

    GameControllerSingleton gc;
    SpawnerController sc;

    // Called at the same time if it is in a sceneload, for all objects being loaded
    // Good to place calcs that are independent from other game objects here
    void Awake()
    {

        if (this.gameObject.name == "redShell")
        {
            meshAgent = GetComponent<NavMeshAgent>();
            navAgent = GetComponent<NavAgentGoToTransform>();
            meshAgent.speed = speed;
            acquireEnemy();
        }
        hitsLeft = hitCounts;

        if (damage <= 0)
        {
            damage = 1;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (!init)
        {
            gc = GameControllerSingleton.get();
            sc = gc.sc;
            inventory = gc.pc.myInventory;
        }

        if ( lifetime <= 0)
        {
            lifetime = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name != "bulletBill" && this.gameObject.name != "redBulletBill")
        {
            transform.Rotate(0, spinFactor, 0);
        }
        age += Time.deltaTime;
        if (age >= lifetime)
        {
            deathSequence();
        }
    }

    // Interesting,
    // FixedUpdate() AddTorque doesn't seem to work with NavMesh and NavAgent
    // 
    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision cInfo)
    {
        EnemyController enemy = cInfo.gameObject.GetComponent<EnemyController>();

        if (cInfo.transform.CompareTag("Enemy"))
        {
            if (--hitsLeft < 1)
            {
                this.gameObject.SetActive(false);
                deathSequence();
            }
            else if (cInfo.gameObject.name == "redShell")
            {
                acquireEnemy();
            }

            enemy.health -= damage;

            if (enemy.health <= 0)
            {
                // Points for now
                gc.pc.adjustScore(enemy.value);
                gc.sc.registerDeadEnemy();
                cInfo.gameObject.SetActive(false);
                Destroy(cInfo.gameObject);
            }
        }
    }

    void acquireEnemy()
    {
        navAgent.target = FindObjectOfType<EnemyController>().transform;
    }

    void deathSequence()
    {
        System.Random rand = new System.Random();
        int itemType = rand.Next(0, 100);
        bool metal = false;
        if (itemType < 50)
        {
            //make ammo
            if (itemType < 25)
            {
                inventory.ammoContents.setAmmo(0, inventory.ammoContents.returnAmmo(0) + 5);
            }else if (itemType < 37)
            {
                inventory.ammoContents.setAmmo(1, inventory.ammoContents.returnAmmo(1) + 5);
            }else if (itemType < 44)
            {
                inventory.ammoContents.setAmmo(2, inventory.ammoContents.returnAmmo(2) + 5);
            }else if (itemType < 48)
            {
                inventory.ammoContents.setAmmo(3, inventory.ammoContents.returnAmmo(3) + 5);
            }else if (itemType < 50)
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
      
            }else if (itemType < 78)
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
            if (metal == false)
            {
                Instantiate(prefab.prefab, transform.position, new Quaternion(0, 0, 0, 0));
            }else
            {

                Instantiate(prefab.prefab, transform.position + new Vector3(0, 3, 0), new Quaternion(0,0,0,0));
            }
        }
        Destroy(gameObject);
    }
}
