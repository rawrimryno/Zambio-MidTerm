using UnityEngine;
using System.Collections;
using System;

public class WaitForChillTimeToEnd : IState
{
    float waited;
    public float waitTime;
    public float minWaitTime, maxWaitTime, defWaitTime = 5.0f;
    GameControllerSingleton gc;
    SpawnerController sc;

    public override void OnStart()
    {
        gc = GameControllerSingleton.get();
        sc = FindObjectOfType<SpawnerController>();
        waited = 0.0f;
        sc.spawning = false;
        if (waitTime < minWaitTime || waitTime > maxWaitTime)
        {
            waitTime = defWaitTime;
        }
    }

    public override void OnUpdate()
    {
        waited += Time.deltaTime;
        if ( waited > waitTime || Input.GetKeyDown(KeyCode.F1))
        {
            this.nextState = this.futureState;
            sc.enemiesSpawned = 0;
            sc.enemiesKilled = 0;
            //sc.enabled = true;
            //sc.getEnemiesThisLevel();
        }
    }
}
