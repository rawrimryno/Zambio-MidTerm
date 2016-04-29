using UnityEngine;

public class AmmoScript : MonoBehaviour
{
    //private Rigidbody rb;
    public int hitCounts;
    public int damage;
    private int hitsLeft;
    private int ID;
    public float speed;
    public float reboundForce;
    bool init = false;
    private Inventory inventory;
    Rigidbody rb;
    PlayerController pc;


    public float spinFactor;
    public float redAccelMultiplier = 7;

    NavMeshAgent meshAgent;
    NavAgentGoToTransform navAgent;

    public float age = 0, lifetime = 5;

    GameControllerSingleton gc;
    //SpawnerController sc;

    float startTime = 0;
    float tripLength = 0;
    Transform startTransform;

    // Called at the same time if it is in a sceneload, for all objects being loaded
    // Good to place calcs that are independent from other game objects here
    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (lifetime <= 0)
        {
            lifetime = 5;
        }

        if (gameObject.name == "redShell")
        {
            meshAgent = GetComponent<NavMeshAgent>();
            navAgent = GetComponent<NavAgentGoToTransform>();
            rb = GetComponent<Rigidbody>();
            meshAgent.speed = speed;
            acquireEnemy();
            //lifetime = 999;  Todd:  Hardcoded values like this makes debugging hard
            // Set it High from the Component Viewer in Unity

            //Debug.Log("RedShell Stupid Check");
        }
        hitsLeft = hitCounts;

        if (damage <= 0)
        {
            damage = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!init)
        {
            gc = GameControllerSingleton.get();
            //inventory = FindObjectOfType<Inventory>();
            inventory = gc.pc.myInventory;
            init = true;
            //Debug.Log("Inventory probably didn't set, don't forget to work on it.");
        }
        if (this.gameObject.name != "bulletBill" && this.gameObject.name != "redBulletBill")
        {
            // Remove deltaTime to bring back spinning during pause
            transform.Rotate(0, spinFactor * Time.deltaTime, 0);
        }
        age += Time.deltaTime;
        if (age >= lifetime)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if (gameObject.name == "redShell")
        {
            if (navAgent.target == null)
            {
                acquireEnemy();
            }
            float distCovered = (Time.time - startTime) * speed;
            float fracTrip = distCovered / tripLength;

            if (navAgent.target != null)
            {
                transform.position = Vector3.Lerp(startTransform.position, navAgent.target.transform.position, fracTrip);
            }

            Vector3 dir = new Vector3();
            dir = navAgent.target.position + navAgent.GetComponent<Rigidbody>().velocity * Time.deltaTime - transform.position;
            rb.velocity = (/*3*rb.mass**/redAccelMultiplier * dir / dir.magnitude);
        }
    }

    // Interesting,
    // FixedUpdate() AddTorque doesn't seem to work with NavMesh and NavAgent
    // 
    void FixedUpdate()
    {
        //if (gameObject.name == "redShell")
        //{
        //    if (navAgent.target == null)
        //    {
        //        acquireEnemy();
        //    }
        //    Vector3 dir = new Vector3();
        //    //if ( navAgent.target.)
        //    dir = navAgent.target.position + navAgent.GetComponent<Rigidbody>().velocity * Time.deltaTime - transform.position;
        //    rb.AddForce(redAccelMultiplier * dir / dir.magnitude);
        //    if ( dir.magnitude < 1)
        //    {
        //        rb.AddForce(dir*redAccelMultiplier);
        //    }
        //}
    }

    void OnCollisionEnter(Collision cInfo)
    {
        EnemyController enemy = cInfo.gameObject.GetComponent<EnemyController>();

        if (cInfo.transform.CompareTag("Enemy"))
        {
            if (--hitsLeft < 1)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);

            }
            else if (gameObject.name == "redShell")
            {
                acquireEnemy();
                rb.AddForce(-rb.velocity * reboundForce * rb.mass);
            }
            if (pc.myPowerUps.Contains("fireFlower"))
            {
                //Debug.Log("Damaging Enemy: " + (damage * 2));
                enemy.health -= (damage * 2);
            }
            else {
                //Debug.Log("Damaging Enemy: " + (damage));
                enemy.health -= damage;
            }


            // Enemy Death Condition
            if (cInfo.gameObject.activeInHierarchy && enemy.health <= 0)
            {
                // Points for now
                gc.pc.adjustScore(enemy.value);
                gc.sc.registerDeadEnemy();
                if (cInfo.gameObject.name == "Bowser")
                {
                    FindObjectOfType<Bowser>().PlayDeathSequence();
                }
                else {
                    cInfo.gameObject.GetComponent<EnemyController>().dying = true;
                }

            }
        }
        else if (cInfo.gameObject.name == "fireBall")
        {
            Homing homer = cInfo.gameObject.GetComponent<Homing>();
            homer.finish = homer.start;
            homer.start = transform.position;
            homer.timeToHit -= Time.time - homer.startTime;
            homer.startTime = Time.time;
            Destroy(gameObject);
        }
    }
    // Computational Complexity : O(numEnemiesInGame^2)
    //  Can reduce to O(n) by saving the enemies to a list as they spawn and searching through that.
    void acquireEnemy()
    {
        const float maxDist = 10000f;
        int best = 0;
        float closest = maxDist;
        float dist;
        EnemyController[] eList;
        eList = FindObjectsOfType<EnemyController>();
        if (eList.Length < 1)
        {
            return;
        }
        for (int i = 0; i < eList.Length; i++)
        {
            dist = Vector3.Distance(transform.position, eList[i].transform.position);
            if (dist < closest)
            {
                best = i;
                closest = dist;
            }
        }
        if (closest == maxDist)
        {
            Destroy(gameObject);
        }
        navAgent.target = eList[best].transform;
        if (navAgent.target.name == "ChompNoRIP")
        {
            navAgent.target = navAgent.target.GetComponent<ChompScript>().headTransform;
        }
        if (navAgent.target.name == "Piranha")
        {
            navAgent.target = navAgent.target.GetComponent<PiranhaScript>().head;
        }
        startTime = Time.time;
        tripLength = Vector3.Distance(navAgent.target.position, transform.position);
        startTransform = this.transform;
    }

    //public void randomDrop()
    //{
    //    System.Random rand = new System.Random();
    //    //150 so 2/3 of the time it will drop a a randomdrop;
    //    int itemType = rand.Next(0, 150);
    //    bool metal = false;
    //    GameObject renameMe;
    //    if (itemType < 50)
    //    {
    //        //make ammo
    //        if (itemType < 25)
    //        {
    //            inventory.ammoContents.setAmmo(0, inventory.ammoContents.returnAmmo(0) + 5);
    //        }
    //        else if (itemType < 37)
    //        {
    //            inventory.ammoContents.setAmmo(1, inventory.ammoContents.returnAmmo(1) + 5);
    //        }
    //        else if (itemType < 44)
    //        {
    //            inventory.ammoContents.setAmmo(2, inventory.ammoContents.returnAmmo(2) + 5);
    //        }
    //        else if (itemType < 48)
    //        {
    //            inventory.ammoContents.setAmmo(3, inventory.ammoContents.returnAmmo(3) + 5);
    //        }
    //        else if (itemType < 50)
    //        {
    //            inventory.ammoContents.setAmmo(4, inventory.ammoContents.returnAmmo(4) + 5);
    //        }
    //    }
    //    else
    //    {
    //        PowerUpDesc prefab = new PowerUpDesc();
    //        //instatiate powerup or mushroom
    //        if (itemType < 65)
    //        {
    //            gc.powerUpByID.TryGetValue(2, out prefab);

    //        }
    //        else if (itemType < 78)
    //        {
    //            gc.powerUpByID.TryGetValue(0, out prefab);

    //        }
    //        else if (itemType < 80)
    //        {
    //            gc.powerUpByID.TryGetValue(1, out prefab);

    //        }
    //        else if (itemType < 90)
    //        {
    //            gc.powerUpByID.TryGetValue(3, out prefab);

    //        }
    //        else if (itemType <= 100)
    //        {
    //            gc.powerUpByID.TryGetValue(4, out prefab);
    //            metal = true;

    //        }
    //        if (itemType <= 100)
    //        {
    //            if (metal == false)
    //            {
    //                renameMe = Instantiate(prefab.prefab, transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
    //                renameMe.name = prefab.prefab.name;
    //            }
    //            else
    //            {
    //                renameMe = Instantiate(prefab.prefab, transform.position + new Vector3(0, 3, 0), new Quaternion(0, 0, 0, 0)) as GameObject;
    //                renameMe.name = prefab.prefab.name;
    //            }
    //        }

    //    }
    //}
}
