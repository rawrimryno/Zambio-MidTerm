using UnityEngine;
using System.Collections;
using System;

public class KillingEnemies : IState {
    GameControllerSingleton gc;
    StateMachine sm;
    SpawnerController sc;

    public override void OnStart()
    {
        gc = GameControllerSingleton.get();
        sm = GetComponentInParent<StateMachine>();
        sc = FindObjectOfType<SpawnerController>();
        sc.enabled = true;
    }

    public override void OnUpdate()
    {
        if ( sc.switchState || Input.GetKeyDown(KeyCode.F1) )
        {
            sc.enabled = false;
            this.nextState = this.futureState;
            sm.Round++;
            sc.switchState = false;

        }
    }

        
}
