using UnityEngine;
using System.Collections;

public class PiranhaController : MonoBehaviour {

    EnemyController ec;

    // Use this for initialization
    void Start () {
        ec = GetComponent<EnemyController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (ec.health <= 0)
        {
            Die();
        }
        print(ec.health);
    }

    public void Die()
    {
        ec.randomDrop();
        Destroy(ec.gameObject);
    }

}
