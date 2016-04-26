using UnityEngine;
using System.Collections;

public class FireAmmo : MonoBehaviour {
    public float maxLife = 10f; // seconds
    public float age = 0f;
    Homing homer;
    // Use this for initialization
	void Start () {
        homer = GetComponentInChildren<Homing>();

    }
	
	// Update is called once per frame
	void Update () {
        age += Time.deltaTime;
        if( age > maxLife)
        {
            homer.gameObject.SetActive(false);
            Destroy(homer.gameObject);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
	}
}
