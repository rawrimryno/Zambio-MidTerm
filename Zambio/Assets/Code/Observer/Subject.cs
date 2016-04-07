using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Subject {
    private List<Observer> observers;

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

    public void Attach(Observer observer)
    {
        observers.Add(observer);
    }
    public void Detach(Observer observer)
    {
        observers.Remove(observer);
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
    public AmmoContents GetState()
    {
        return ammo;
    }
    public void SetState( AmmoContents inAmmo)
    {
        ammo = inAmmo;
    }
}