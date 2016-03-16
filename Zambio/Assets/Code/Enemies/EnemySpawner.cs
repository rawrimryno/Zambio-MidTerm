using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject Enemy;
    public float rate;
    public float level;
    void Start()
    {
            InvokeRepeating("Spawn",1,rate);   
    }

    void Spawn()
    {
        float rand = Random.Range(2, 5) * Mathf.Pow(-1,Random.Range(2, 5));
        Instantiate(Enemy, new Vector3(transform.position.x + rand, transform.position.y, transform.position.z + rand), transform.rotation);
    }
}