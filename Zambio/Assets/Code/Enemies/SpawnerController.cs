using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    public int maxEnemiesOnScreen = 50;  // To be determined, ~MaxEnemiesInScene

    public int enemiesThisLevel;
    public int levelMultiple = 10;

    public int enemiesLeft;
    public int enemiesSpawned;
    public int enemiesKilled { get; set; }
    public bool spawning = true;
    public bool switchState = false;

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
        getEnemiesThisLevel();
        Debug.Log("Stupid Check");
        enemiesSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // All init observers should be attached
        if ( !oInit)
        {
            spawnSubject.Notify();
        }

        // Has the player finished killing?
        if ( spawning && enemiesKilled == enemiesThisLevel )
        {
            switchState = true;
            spawning = false;
            //enemiesLeft = 0;
            spawnSubject.Notify();
        }
    }

    public void getEnemiesThisLevel()
    {
        enemiesThisLevel = 5*(FindObjectOfType<StateMachine>().Round);
        enemiesLeft = enemiesThisLevel;
    }

    public bool canSpawn()
    {
        bool status = false;

        getEnemiesThisLevel();

        if (enemiesSpawned < enemiesThisLevel)
        {
            status = true;
            spawning = true;
        }
        return status;
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
