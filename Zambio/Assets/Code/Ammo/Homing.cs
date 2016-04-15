using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour
{
    public Transform target;
    public int damage = 1;
    Vector3 path;
    float distance, progress;
    Vector3 start, finish;
    float startTime;
    public float timeToHit = 5;

    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        start = gameObject.transform.position;
        finish = target.position + target.GetComponent<Rigidbody>().velocity * timeToHit;
        path = target.position - start;
        distance = path.magnitude;
        progress = 0;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        progress = (Time.time - startTime) / timeToHit;
        if ( gameObject.name == "onFire") {
            // Practically "stick" to the target, which should be the attached ammo
            transform.position = Vector3.Lerp(transform.position, target.position, progress);
        }

        transform.position = Vector3.Lerp(start, finish, progress);
    }
    void OnTriggerEnter(Collider tColl )
    {
        if ( gameObject.name == "fireBall" &&  tColl.CompareTag("Player"))
        {
            tColl.gameObject.GetComponent<PlayerController>().adjustHealth(-damage);
        }
    }
}

