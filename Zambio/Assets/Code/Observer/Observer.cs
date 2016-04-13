using UnityEngine;
using System.Collections;
using System;

public abstract class Observer {
    public abstract void update();
}

public class SpawnControllerObserver : Observer
{
    //public bool canSpawn = false;
    public SpawnSubject spawnSubject;
    private SpawnerController spawnController; // For registerNewEnemy

    public SpawnControllerObserver()
    {
        //spawnSubject = new SpawnSubject();
        //spawnController = new SpawnerController();
    }

    public override void update()
    {
        spawnController = spawnSubject.GetState();
        //spawner.canSpawn = spawnSubject.GetState().canSpawn();
    }
}

public class UIAmmoObserver : Observer
{
    public AmmoSubject ammoSubject;
    //public AmmoPanel ammoPanel;  // or w/e
    AmmoContents ammoState;
    
    public override void update()
    {
        ammoState = ammoSubject.GetState();
        //ammoPanel.setAmmo( ammoState ); // pr w/e
    }
}

public class UIHealthObserver : Observer
{
    private int health;
    public HealthSubject healthSubject;
    public HealthPanel healthPanel;

    public override void update()
    {
        health = healthSubject.GetState();
        healthPanel.setHearts( health );
    }
}