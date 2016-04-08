using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    public int maxEnemiesOnScreen = 50;  // To be determined, ~MaxEnemiesInScene

    public int enemiesThisLevel;
    public int levelMultiple = 10;

    public int enemiesLeft { get; private set; }
    public int enemiesKilled { get; private set; }
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
        getEnemiesThisLevel();
        spawnSubject = new SpawnSubject();
        spawnSubject.SetState(this);
        enemiesLeft = 0;
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
        if ( spawning && enemiesLeft == -1 )
        {
            switchState = true;
            spawning = false;
            enemiesLeft = 0;
        }
    }

    public void getEnemiesThisLevel()
    {
        if (!gc.sm)
        {
            enemiesThisLevel = gc.sm.Round * levelMultiple;
        }
        else
        {
            enemiesThisLevel = 20; 
        }
    }

    public bool canSpawn()
    {
        bool status = false;
        if (enemiesLeft+enemiesKilled > enemiesThisLevel)
        {
            status = true;
            spawning = true;
        }
        return status;
    }

    public void registerDeadEnemy()
    {
        enemiesLeft--;
    }

    public void registerNewEnemy()
    {
        enemiesLeft++;
    }

}
