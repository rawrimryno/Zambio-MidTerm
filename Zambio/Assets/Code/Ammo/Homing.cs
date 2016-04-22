using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour
{
    public Transform target;
    public int damage = 1;
    Vector3 path;
    float distance, progress;
    public Vector3 start, finish;
    public float startTime;
    public float timeToHit = 2;

    // Use this for initialization
    void Start()
    {
        if (gameObject.name == "fireBall")
        {
            target = FindObjectOfType<PlayerController>().transform;
        }

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
        if (gameObject.name == "onFire")
        {
            // Practically "stick" to the target, which should be the attached ammo
            if (target != null)
                transform.position = Vector3.Lerp(transform.position, target.position, progress);
        }
        else { // Fireball
            transform.position = Vector3.Lerp(start, finish, progress);
        }
    }
    void OnTriggerEnter(Collider tColl)
    {
        if (gameObject.name == "fireBall")
        {
            if (tColl.CompareTag("Player"))
            {
                tColl.gameObject.GetComponent<PlayerController>().adjustHealth(-damage);
            }
            else if (tColl.CompareTag("Bowser"))
            {
                tColl.gameObject.GetComponent<EnemyController>().health -= damage / 2;
            }

        }
    }
    void OnCollisionEnter( Collision cInfo)
    {
        if (gameObject.name == "fireBall")
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

