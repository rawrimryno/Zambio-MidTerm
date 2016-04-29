using UnityEngine;
using System.Collections;

public class NavAgentGoToTransform : MonoBehaviour
{

    public Transform target;
    NavMeshAgent agent;
    private float lookRatio = 1;
    GameControllerSingleton gc;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        gc = GameControllerSingleton.get();
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            if ( gameObject.name != "redShell" )
            {
                agent.SetDestination(target.position);
                //   transform.LookAt(new Vector3 (target.position.x, gc.pc.GetComponentInChildren<Camera>().transform.position.y, target.position.z));
                transform.LookAt(new Vector3(target.position.x, 3, target.position.z));
            }
        }
    }
}
