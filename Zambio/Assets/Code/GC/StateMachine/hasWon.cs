using UnityEngine;
using System.Collections;

public class hasWon : IState {

	// Use this for initialization
	public override void OnStart () {
	    // Display has Won Message
	}
	
	// Update is called once per frame
	public override void OnUpdate () {
	    // Do nothing really. at this point?  Maybe ask if they want to go
        // for free for all?  Let rounds update until they die?
        // In that case, future state neeeds to be set to Killing
        // After the player has chosen to continue on.
	}
}
