using UnityEngine;
using System.Collections;

public class Homing : MonoBehaviour
{
    public Transform target;
    Vector3 path;
    float distance, progress;
    Vector3 start;

    // Use this for initialization
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        start = gameObject.transform.position;
        path = target.position - start;
        distance = path.magnitude;
        progress = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(start, target.position, progress);
        progress = (target.position - start ).magnitude / distance;
    }
}

