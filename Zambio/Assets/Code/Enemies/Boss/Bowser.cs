using UnityEngine;
using System.Collections;

public class Bowser : MonoBehaviour {
    public float flareAttackRate;
    public float fireBallAttackRate;
    private float[] attackTimers;
    public GameObject Flare;
    public GameObject FireBall;
    public Transform mouth;

	// Use this for initialization
	void Start () {
        attackTimers = new float[3];
        for(int i = 0; i < 2; i++)
        {
            attackTimers[i] = 0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 2; i++)
        {
            attackTimers[i] += Time.deltaTime;
        }
        if ( attackTimers[0] >= flareAttackRate)
        {
            attackTimers[0] = 0f;
            //Flare Attack
        }
        else if ( attackTimers[1] >= fireBallAttackRate) {
            Debug.Log("Bowser is shooting a fireball!");
            GameObject thisOne;
            thisOne = Instantiate(FireBall, mouth.transform.position, Quaternion.identity) as GameObject;
            attackTimers[1] = 0f;
        }
	}
}
