using UnityEngine;
using System.Collections;

public class FireColorChanger : MonoBehaviour {
    public Gradient greenGrad;
    public Gradient redGrad;
    public Gradient blueGrad;
    public Gradient bulletGrad;
    public Gradient redBulletGrad;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public Gradient getGrad(int num)
    {
        Gradient tmp;
        switch (num)
        {
            case 1: tmp = greenGrad; break;
            case 2: tmp = redGrad; break;
            case 3: tmp = blueGrad; break;
            case 4: tmp = bulletGrad; break;
            case 5: tmp = redBulletGrad; break;
            default: tmp = null; break;
        }
        return tmp;
    }
}
