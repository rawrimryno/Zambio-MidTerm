using UnityEngine;
using System.Collections;
using System;

public class KillingEnemies : IState {
    GameControllerSingleton gc;
    StateMachine sm;
    public SpawnControllerObserver spawnO;


    public override void OnStart()
    {
        gc = GameControllerSingleton.get();
        sm = GetComponentInParent<StateMachine>();
        spawnO = new SpawnControllerObserver();
        gc.sc.spawnSubject.Attach(spawnO);

        //sc = FindObjectOfType<SpawnerController>();
        //sc.enabled = true;
    }

    public override void OnUpdate()
    {
        // Change state Conditions
        if ( Input.GetKeyDown(KeyCode.F1) ) // spawnO is null in here
        {
            spawnO.spawnController.enabled = false;
            spawnO.spawnController.switchState = false;
            sm.Round++;
            this.nextState = this.futureState;
        }
    }

        
}
