using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject Enemy;
    public float rate;
    //public float level;
    //public SpawnerController master;
    SpawnControllerObserver spawnObserver;

    public bool registered=false;
    public bool canSpawn = false;

    void Start()
    {
        spawnObserver = new SpawnControllerObserver();

        //master = FindObjectOfType<SpawnerController>();
        //level = master.enemiesThisLevel / master.levelMultiple;
    }

    void Update()
    {
        if ( !registered )
        {
            if (FindObjectOfType<SpawnerController>().spawnSubject.Attach(spawnObserver))
            {
                registered = true;
                InvokeRepeating("Spawn", 1, rate);
            }
        }
    }

    void Spawn()
    {
        float rand = Random.Range(2, 5) * Mathf.Pow(-1,Random.Range(2, 5));
        canSpawn = spawnObserver.spawnSubject.GetState().canSpawn();
        if ( canSpawn )
        {
            Instantiate(Enemy, new Vector3(transform.position.x + rand, transform.position.y, transform.position.z + rand), transform.rotation);
            spawnObserver.spawnSubject.GetState().registerNewEnemy();
        }
    }
}