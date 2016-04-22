using UnityEngine;
using System.Collections;

public class Bowser : MonoBehaviour {
    public float flareAttackRate;
    public float fireBallAttackRate;
    public GameObject Flare;
    public GameObject FireBall;
    public Transform mouth;
    public MainMenu UImain;
    public AudioClip[] effectList;

    private float[] attackTimers;
    BossSubject bossSub;
    EnemyController ec;
    StateMachine sm;
    int currHealth;
    AudioSource bowserSource;
    bool hasPlayedDeathSound = false;
    Rigidbody rb;


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
        UImain = FindObjectOfType<MainMenu>();
        //UImain.bossObserver().attach(bossSub);
        bossSub.Notify();
        sm = GameObject.Find("_GameStateMachine").GetComponent<StateMachine>();

        // Audio Source Acquisition - Todd
        bowserSource = GetComponent<AudioSource>();
        bowserSource.clip = effectList[0];
        bowserSource.Play();

        rb = GetComponent<Rigidbody>();
        currHealth = sm.Round * 10;
        ec.health = currHealth;
        Debug.Log("Bowser Health: " + currHealth);
	}
	
	// Update is called once per frame
	void Update () {
        if ( currHealth != ec.health)
        {
            currHealth = ec.health;
            bossSub.Notify();
        }
        // Removed Destroy from Ammo for bowser so that he may play his death sound - Todd
        if (currHealth <= 0)
        {
            // Stop Motion on Death so he doesn't slide around when he should be dying.
            rb.constraints = RigidbodyConstraints.FreezeAll;
            // ^ Wasn't very effective.
            if( hasPlayedDeathSound && bowserSource.isPlaying == false ) // Add Condition for Animation.isPlaying == false, to allow animation to stop before destroy -Todd
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
                
            }
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
        else if ( currHealth > 0 && attackTimers[1] >= fireBallAttackRate) {
            Debug.Log("Bowser is shooting a fireball!");
            GameObject thisOne;
            thisOne = Instantiate(FireBall, mouth.transform.position, Quaternion.identity) as GameObject;
            thisOne.name = FireBall.name;
            attackTimers[1] = 0f;

            // Audio for FireBall Shot
            bowserSource.Stop();
            bowserSource.clip = effectList[1];
            bowserSource.Play();
        }
	}
    public void AddObserver( BossObserver bo)
    {
        bossSub.Attach(bo);
    }
    public void RemoveObserver( BossObserver bo)
    {
        bossSub.Detach(bo);
    }

    public void PlayDeathSequence()
    {
        bowserSource.Stop();
        bowserSource.clip = effectList[2];
        bowserSource.Play();
        hasPlayedDeathSound = true;
        Debug.Log("Playing Bowser Death Sound");
        hasPlayedDeathSound = true;
    }
}
