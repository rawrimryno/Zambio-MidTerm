using UnityEngine;
using System.Collections;
using System;

public class KillingEnemies : IState {
    GameControllerSingleton gc;
    StateMachine sm;
    public SpawnControllerObserver spawnO;
    bool oInit = false;

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
            this.nextState = this.futureState;
        }
    }

        
}
