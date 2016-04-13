using UnityEngine;
using System.Collections;

public class BossEncounter : IState {
    public GameObject Boss;
    public GameObject spawnLocation;
	// Use this for initialization
	public override void OnStart () {
        // Load Boss
        Debug.Log("Load Boss");
        Instantiate(Boss, spawnLocation.transform.position, spawnLocation.transform.rotation);
	
	}

    // Update is called once per frame
    public override void OnUpdate() {
        // Check if Boss Has been Killed
        // true : this.nextState = this.futureState, where future state is hasWon

	}
}
