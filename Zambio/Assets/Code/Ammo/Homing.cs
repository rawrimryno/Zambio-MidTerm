using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour
{
    public Transform target;
    Vector3 path;
    float distance, progress;
    Vector3 start;
    float startTime;
    public float timeToHit = 5;

    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        start = gameObject.transform.position;
        path = target.position - start;
        distance = path.magnitude;
        progress = 0;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        progress = (Time.time - startTime) / timeToHit;
        transform.position = Vector3.Lerp(start, target.position, progress);
    }
}

