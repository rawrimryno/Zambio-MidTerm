using UnityEngine;
using System.Collections;

public class MarioWalk : MonoBehaviour {
    PlayerController pc;
    Animator anim;
    PlayerMovement pm;

	// Use this for initialization
	void Start () {
        pc = GetComponentInParent<PlayerController>();
        anim = GetComponent<Animator>();
        pm = GetComponentInParent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {
	    if (pm.moveDirection.magnitude >= 1)
        {
            if (pc.isMetalMario)
            {
                anim.SetBool("isMetal", true);
               // Debug.Log("Metal Walking");
            }
            else
            {
                anim.SetBool("isMetal", false);
                //Debug.Log("Regular Walking");
            }
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
            //Debug.Log("Not Walking");
        }
    }
}
