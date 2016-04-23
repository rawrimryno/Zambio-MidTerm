using UnityEngine;
using System.Collections;

public class EnemyAnimator : MonoBehaviour {
    Rigidbody rb;
    Animator anim;
    Animation animation;
    EnemyController ec;
    bool dead;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        ec = GetComponent<EnemyController>();
	
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
            anim.SetBool("isDying", true);
            animation = anim.GetComponent<Animation>();
        }

        if (anim.GetBool("isDying") == true && !animation.isPlaying)
        {
            ec.gameObject.SetActive(false);
            Destroy(ec.gameObject);
            AmmoScript dummyAmmo = new AmmoScript();
            dummyAmmo.randomDrop();
        }
        Debug.Log("Enemy Velocity is " + rb.velocity.magnitude);
	
	}
}
