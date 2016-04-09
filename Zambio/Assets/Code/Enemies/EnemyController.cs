using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    GameControllerSingleton gc;
    NavAgentGoToTransform enemyNav;
    Rigidbody rb;

    private bool hasLeftSpawner = true;
    private bool init = false;
    private float yDisp = 0;

    public float pipeHeight = 5;

    public int health;
    public int damage;
    public int value;
    public float hitTime;  // time for one hit
    private float coolDown=0;

    void Awake()
    {
        Random.seed = (int)Time.realtimeSinceStartup;

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
        gc = GameControllerSingleton.get();
        enemyNav = GetComponent<NavAgentGoToTransform>();
        rb = GetComponent<Rigidbody>();
        acquirePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }
        if (!gc)
        {
            gc = GameControllerSingleton.get();
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
            if (coolDown <= 0)
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
}