using UnityEngine;
using System.Collections;
using System;

public abstract class Observer {
    public abstract void update();
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