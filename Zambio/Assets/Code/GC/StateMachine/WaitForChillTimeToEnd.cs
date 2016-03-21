using UnityEngine;
using System.Collections;
using System;

public class WaitForChillTimeToEnd : IState
{
    float waited;
    public float waitTime;
    public float minWaitTime, maxWaitTime, defWaitTime = 5.0f;
    GameControllerSingleton gc;

    public override void OnStart()
    {
        gc = GameControllerSingleton.get();
        waited = 0.0f;
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
            gc.sc.getEnemiesThisLevel();
        }
    }
}
