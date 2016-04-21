using UnityEngine;
using System.Collections;
using System;

public class KillingEnemies : IState {
    GameControllerSingleton gc;
    StateMachine sm;
    public SpawnControllerObserver spawnO;
    public int bossWave = 10;
    bool oInit = false;
    public IState bossState;
    public IState ChillState;

    public override void OnStart()
    {
        gc = GameControllerSingleton.get();
        sm = GetComponentInParent<StateMachine>();
        if ( oInit )
        {
            spawnO.spawnSubject.GetState().initSpawner();
        }
    }

    public override void OnUpdate()
    {
        if (!oInit)
        {
            OnStart();
            spawnO = new SpawnControllerObserver();
            gc.sc.spawnSubject.Attach(spawnO);
            spawnO.spawnSubject.GetState().initSpawner();
            oInit = true;
        }
        // Change state Conditions
        if ( spawnO.spawnSubject.GetState().endRound || Input.GetKeyDown(KeyCode.F1) ) // spawnO is null in here
        {
            // Finished Killing:
            // Increment Round, Set Switch Condition to false
            // 
            spawnO.spawnSubject.GetState().endRound = false;
            sm.Round++;
            if (sm.Round == bossWave)
            {
                this.futureState = bossState;
            }
            else
            {
                this.futureState = ChillState;
            }
            this.nextState = this.futureState;
        }
    }

        
}
