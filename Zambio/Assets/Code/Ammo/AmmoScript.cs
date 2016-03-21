﻿using UnityEngine;
using System.Collections;
using System;

public class AmmoScript : MonoBehaviour
{
    //private Rigidbody rb;
    public int hitCounts;
    private int hitsLeft;
    private int ID;
    public float speed;
    bool init = false;


    public float spinFactor;

    NavMeshAgent meshAgent;
    NavAgentGoToTransform navAgent;

    public float age=0, lifetime=4;

    GameControllerSingleton gc;
    SpawnerController sc;

    // Called at the same time if it is in a sceneload, for all objects being loaded
    // Good to place calcs that are independent from other game objects here
    void Awake()
    {

        if (this.gameObject.name == "redShell")
        {
            meshAgent = GetComponent<NavMeshAgent>();
            navAgent = GetComponent<NavAgentGoToTransform>();
            meshAgent.speed = speed;
            acquireEnemy();
        }
        hitsLeft = hitCounts;
    }

    // Use this for initialization
    void Start()
    {
        if (!init)
        {
            gc = GameControllerSingleton.get();
            sc = gc.sc;
        }

        if ( lifetime <= 0)
        {
            lifetime = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.name != "bulletBill" && this.gameObject.name != "redBulletBill")
        {
            transform.Rotate(0, spinFactor, 0);
        }
        age += Time.deltaTime;
        if (age >= lifetime)
        {
            deathSequence();
        }
    }

    // Interesting,
    // FixedUpdate() AddTorque doesn't seem to work with NavMesh and NavAgent
    // 
    void FixedUpdate()
    {

    }

    void OnCollisionEnter(Collision cInfo)
    {
        if (cInfo.transform.CompareTag("Enemy"))
        {
            if (--hitsLeft < 1)
            {
                this.gameObject.SetActive(false);
                deathSequence();
            }
            else if (cInfo.gameObject.name == "redShell")
            {
                acquireEnemy();
            }

            // Points for now
            gc.pc.adjustScore(10);
            gc.sc.registerDeadEnemy();
            cInfo.gameObject.SetActive(false);
            Destroy(cInfo.gameObject);
        }
    }

    void acquireEnemy()
    {
        navAgent.target = FindObjectOfType<EnemyController>().transform;
    }

    void deathSequence()
    {
        Destroy(gameObject);
    }
}
