﻿using UnityEngine;
using System.Collections;

public class PeachHeal : MonoBehaviour {

    GameControllerSingleton gc;
    public float healWait;
    public float healTime;
    public float coolDown;
    public int healAmount;
    private float wait;
    private float initCoolDown;
    private float time;
    private bool waiting;
    ParticleSystem particle;


	// Use this for initialization
	void Start () {
        gc = GameControllerSingleton.get();
        particle = GetComponentInChildren<ParticleSystem>();
        waiting = true;

        if (healWait == 0)
        {
            healWait = 30f;
        }
        if (healTime == 0)
        {
            healTime = 12f;
        }
        if (healAmount == 0)
        {
            healAmount = 1;
        }
        if (coolDown == 0)
        {
            coolDown = 1f;
        }
        initCoolDown = coolDown;
    }
	
	// Update is called once per frame
	void Update () {
        if (!gc)
        {
            gc = GameControllerSingleton.get();
        }
        if (Time.timeScale != 0f)
        {
            if (waiting)
            {
                wait -= Time.deltaTime;
                if (wait <= 0)
                {
                    waiting = false;
                    wait = healWait;
                    particle.Play();
                }
                Debug.Log("Stupid.Check-waiting");
            }
            else
            {
                time -= Time.deltaTime;
                if (time <= 0 && Time.timeScale == 1f)
                {
                    waiting = true;
                    time = healTime;
                    particle.Clear();
                    particle.Pause();
                }

                Debug.Log("Stupid.Check-healing");
            }

        }
        coolDown -= Time.deltaTime;
    }

    void OnTriggerStay(Collider oCol)
    {
        if (!waiting)
        {
            if (oCol.CompareTag("Player"))
            {
                if (coolDown <= 0)
                {
                    healPlayer();
                }
            }
        }
    }
    void healPlayer()
    {
        // Debug.Log("Hurting Player");
        gc.pc.adjustHealth(healAmount);
        coolDown = initCoolDown;
    }
}