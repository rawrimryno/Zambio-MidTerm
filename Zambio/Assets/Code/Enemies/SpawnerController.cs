using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    public int maxEnemiesOnScreen = 50;  // To be determined, ~MaxEnemiesInScene

    public int enemiesThisLevel;
    public int levelMultiple = 10;

    //public int enemiesLeft;
    public int enemiesSpawned;
    public int enemiesKilled;
    public bool spawning;
    public bool endRound;

    GameControllerSingleton gc;

    public SpawnSubject spawnSubject;

    private bool oInit = false; // observer's initial broadcast

    // Use this for initialization
    void Start()
    {
        gc = GameControllerSingleton.get();
        var temp = this;
        gc.RegisterSpawner(ref temp);
        spawnSubject = new SpawnSubject();
        spawnSubject.SetState(this);
        initSpawner();
        //getEnemiesThisLevel();
        Debug.Log("Stupid Check");
        //enemiesSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // All init observers should be attached
        if ( !oInit )
        {
            spawnSubject.Notify();
            oInit = true;
        }

        // Increment Round Condition
        if ( spawning && enemiesKilled >= enemiesThisLevel )
        {
            //Set conditions for observer on State Machine to switch and disable anymore spawning
            endRound = true;
            spawning = false;
            spawnSubject.Notify();
        }
    }

    public void getEnemiesThisLevel()
    {
        enemiesThisLevel = 5*(FindObjectOfType<StateMachine>().Round);
        //enemiesLeft = enemiesThisLevel;
    }

    // SpawnerController::canSpawn()
    // ret type:  bool
    // Let's a EnemySpawner know if it can spawn an enemy
    public bool canSpawn()
    {
        bool status = false;

        //getEnemiesThisLevel();

        if (spawning && enemiesSpawned < enemiesThisLevel)
        {
            status = true;
            //spawning = true;
        }
        return status;
    }

    public void initSpawner()
    {
        enemiesSpawned = 0;
        endRound = false;
        getEnemiesThisLevel();
        enemiesKilled = 0;
        spawning = true;

    }

    public void registerDeadEnemy()
    {
        enemiesKilled++;
    }

    public void registerNewEnemy()
    {
        enemiesSpawned++;
    }

}
