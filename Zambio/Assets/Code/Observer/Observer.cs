using UnityEngine;
using System.Collections;
using System;

public abstract class Observer {
    public abstract void update();
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