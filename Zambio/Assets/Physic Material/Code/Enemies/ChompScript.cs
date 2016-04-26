using UnityEngine;
using System.Collections;

public class ChompScript : MonoBehaviour {
    public Transform headTransform;
    public float attackRadius;
    private float distance;
    EnemyController ec;
    Animator anim;

	// Use this for initialization
	void Start () {
        ec = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(this.transform.position, ec.pc.gameObject.transform.position);
        if (ec.health > 0)
        {
            if (distance <= attackRadius)
            {
                Debug.Log("Attacking");
                anim.SetBool("isAttacking",true);
            }
            else
            {
                Debug.Log("Idle");
                anim.SetBool("isAttacking", false);
            }
        }
        else
        {
            anim.SetBool("isAttacking", false);
            anim.SetBool("isDying", true);
        }
        

        if (ec.health <= 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Flatten"))
        {
            StartCoroutine("Die");
        }
	}

    IEnumerator Die()
    {
        Debug.Log("Starting Coroutine");
        //anim.SetInteger("Flatten", 3);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        ec.randomDrop();
        Destroy(ec.gameObject);
    }
}
