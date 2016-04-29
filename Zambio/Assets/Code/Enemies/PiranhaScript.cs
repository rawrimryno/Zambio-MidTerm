﻿using UnityEngine;
using System.Collections;

public class PiranhaScript : MonoBehaviour
{
    private Transform target;
    public Transform head;
    public GameObject FireBall;
    public float attackRadius;
    public Transform mouth;
    public float fireballRadius;
    public float fireballPeriod;
    private float distance;
    private float fireballTimer =0f;
    EnemyController ec;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        ec = GetComponent<EnemyController>();
        anim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        fireballTimer += Time.deltaTime;
        transform.LookAt(new Vector3(target.position.x, 3, target.position.z));
        distance = Vector3.Distance(this.transform.position, ec.pc.gameObject.transform.position);
        if (ec.health > 0)
        {
            if (distance <= attackRadius)
            {
                //Debug.Log("Attacking");
                anim.SetBool("isAttacking", true);
            }
            else if (distance <= fireballRadius && fireballTimer >= fireballPeriod)
            {
                GameObject thisOne;
                thisOne = Instantiate(FireBall, mouth.transform.position, Quaternion.identity) as GameObject;
                thisOne.name = FireBall.name;
                fireballTimer = 0f;

            }
            else
            {
                //Debug.Log("Idle");
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
            Destroy(ec.gameObject.GetComponentInChildren<CapsuleCollider>());
            StartCoroutine("Die");
        }
    }

    IEnumerator Die()
    {
        //Debug.Log("Starting Coroutine");
        //anim.SetInteger("Flatten", 3);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        ec.randomDrop();
        Destroy(ec.gameObject);
    }
}
