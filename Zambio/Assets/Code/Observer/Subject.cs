using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Subject {
    protected List<Observer> observers;

    public Subject()
    {
        observers = new List<Observer>();
    }

    public void Notify()
    {
        int count = observers.Count;
        Observer o;

        for ( int i = 0; i < count; i++) {
            o = observers[i];
            o.update();
        }
    }

    public virtual bool Attach(Observer observer)
    {
        observers.Add(observer);
        return observers.Contains(observer);
    }
    public void Detach(Observer observer)
    {
        observers.Remove(observer);
    }
}

public class SpawnSubject : Subject
{
    private SpawnerController state;
    private GameControllerSingleton gc;

    //public SpawnSubject():base()
    //{
    //    gc = GameControllerSingleton.get();
    //    state = gc.sc;
    //}

    public bool Attach(SpawnControllerObserver observer)
    {
        observers.Add(observer);
        observer.spawnSubject = this;
        observer.update();
        //observer.spawnController 
        return observers.Contains(observer);
    }

    public SpawnerController GetState()
    {
        return state;
    }
    public void SetState( SpawnerController inSpawner)
    {
        //state = new SpawnerController();
        state = inSpawner;
    }
}
public class HealthSubject : Subject
{
    //public HealthSubject()
    //{
    //    observers = new List<Observer>();
    //}


    private int health;
    public int GetState() {
        return health;
    }
    public void SetState( int inH )
    {
        health = inH;
    }
}

public class AmmoSubject : Subject
{
    private AmmoContents ammo;

    void Awake()
    {
        ammo = new AmmoContents();
    }
    public AmmoContents GetState()
    {
        return ammo;
    }
    public void SetState( AmmoContents inAmmo)
    {
        ammo = inAmmo;
    }
}