using UnityEngine;
using System.Collections;

public class BossEncounter : IState {
    public GameObject Boss;
    GameObject clone;
    public GameObject spawnLocation;
    GameControllerSingleton gc;
	// Use this for initialization
	public override void OnStart () {
        // Load Boss
        Debug.Log("Load Boss");
        clone = Instantiate(Boss, spawnLocation.transform.position, spawnLocation.transform.rotation) as GameObject;
        gc = GameControllerSingleton.get();
        gc.setBossSpawned();
	
	}

    // Update is called once per frame
    public override void OnUpdate() {
        // Check if Boss Has been Killed
        // true : this.nextState = this.futureState, where future state is hasWon
        if ( clone == null)
        {
            this.nextState = this.futureState;
        }
	}
}
