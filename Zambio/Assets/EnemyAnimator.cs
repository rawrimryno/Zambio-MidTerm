using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour {
    Rigidbody rb;
    Animator anim;
    //Animation animation;
    EnemyController ec;
    GameControllerSingleton gc;
    //AmmoScript dummyAmmo;
    bool dead;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        ec = GetComponent<EnemyController>();
        gc = GameControllerSingleton.get();
       // dummyAmmo = ec.gameObject.AddComponent<AmmoScript>();

    }
	
	// Update is called once per frame
	void Update () {
        if (ec.health > 0)
        {
            anim.SetBool("isDying", false);
            if (rb.velocity.magnitude > 0.02)
            {
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }
        }
        else
        {
            anim.SetBool("isMoving", false);
            anim.SetBool("isDying", true);
        }

        if (ec.health <= 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Flatten"))
        {
            //ec.gameObject.SetActive(false);
            //Destroy(ec.gameObject);
            //AmmoScript dummyAmmo = new AmmoScript();
            //dummyAmmo.randomDrop();
            Destroy(ec.gameObject.GetComponent<CapsuleCollider>());
            StartCoroutine("Die");
        }
        //Debug.Log("Enemy Velocity is " + rb.velocity.magnitude);
	
	}

    IEnumerator Die()
    {
        Debug.Log("Starting Coroutine");
        //anim.SetInteger("Flatten", 3);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        if (this.gameObject.name == "Bowser")
        {
            gc.setBossDead();
            gc.setBossSpawned();
        }
        ec.randomDrop();
        Destroy(ec.gameObject);
    }
}
