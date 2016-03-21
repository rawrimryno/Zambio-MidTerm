using UnityEngine;
using System.Collections;
using System;

public class KillingEnemies : IState {
    GameControllerSingleton gc;
    StateMachine sm;

    public override void OnStart()
    {
        gc = GameControllerSingleton.get();
        sm = GetComponentInParent<StateMachine>();
        gc.sc.enabled = true;
    }

    public override void OnUpdate()
    {
        if ( gc.sc.switchState || Input.GetKeyDown(KeyCode.F1) )
        {
            gc.sc.enabled = false;
            this.nextState = this.futureState;
            sm.Round++;
            gc.sc.switchState = false;

        }
    }

        
}
