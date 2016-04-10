using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

    public IState currentState;
    public int Round = 1;

    public void SetState(IState newState)
    {

        currentState.enabled = false;
        currentState = newState;
        currentState.OnStart();
        currentState.enabled = true;
        currentState.nextState = null;

    }

	// Update is called once per frame
	void Update () {
        IState gotoState = currentState.nextState;
        if (currentState != gotoState && gotoState != null)
        {
            SetState(gotoState);
        }
	}
}
