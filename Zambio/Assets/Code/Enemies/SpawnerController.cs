using UnityEngine;
using System.Collections;

public class SpawnerController : MonoBehaviour
{
    public int maxEnemiesOnScreen = 50;  // To be determined, ~MaxEnemiesInScene

    public int enemiesThisLevel;
    public int levelMultiple = 10;
    public int sceneEnemies;

    public int currEnemies { get; private set; }
    public int deadEnemies { get; private set; }

    public bool switchState = false;

    GameControllerSingleton gc;

    // Use this for initialization
    void Start()
    {
        gc = GameControllerSingleton.get();
        var temp = this;
        gc.RegisterSpawner(ref temp);
        getEnemiesThisLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (deadEnemies != 0 && deadEnemies >= currEnemies + sceneEnemies)
        {
            deadEnemies = 0;
            sceneEnemies = 0;
            switchState = true;
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
            enemiesThisLevel = 10; 
        }
    }

    public bool canSpawn()
    {
        bool status = false;
        if (currEnemies < maxEnemiesOnScreen && currEnemies < enemiesThisLevel)
        {
            status = true;
        }
        currEnemies++;
        return status;
    }

    public void registerDeadEnemy()
    {
        deadEnemies++;
    }

}
