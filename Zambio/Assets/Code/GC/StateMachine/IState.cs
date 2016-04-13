using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IState : MonoBehaviour {
    public GameObject stateGameObject;
    public IState nextState;
    public IState futureState;
    public Stack<IState> gameHistory;
	// Use this for initialization
	void Start () {
        nextState = this;
	}
	
	// Update is called once per frame
	void Update () {
        this.OnUpdate();
    }
    public abstract void OnStart();
    public abstract void OnUpdate();
}

