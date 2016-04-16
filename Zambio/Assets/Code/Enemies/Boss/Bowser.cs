using UnityEngine;
using System.Collections;

public class Bowser : MonoBehaviour {
    public float flareAttackRate;
    public float fireBallAttackRate;
    private float[] attackTimers;
    public GameObject Flare;
    public GameObject FireBall;
    public Transform mouth;
    public MainMenu UImain;
    BossSubject bossSub;
    EnemyController ec;
    int currHealth;

	// Use this for initialization
	void Start () {
        attackTimers = new float[3];
        for(int i = 0; i < 2; i++)
        {
            attackTimers[i] = 0f;
        }
        bossSub = new BossSubject();
        ec = GetComponent<EnemyController>();
        bossSub.SetState(ec);
        currHealth = ec.health;
        UImain = FindObjectOfType<MainMenu>();
        //UImain.bossObserver().attach(bossSub);
        bossSub.Notify();
	}
	
	// Update is called once per frame
	void Update () {
        if ( currHealth != ec.health)
        {
            currHealth = ec.health;
            bossSub.Notify();
        }

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
            thisOne.name = FireBall.name;
            attackTimers[1] = 0f;
        }
	}
}
