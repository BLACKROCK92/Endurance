using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavScript : MonoBehaviour
{

    public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
    public Transform target;                                    // target to aim for


    private void Start()
    {
        // get the components on the object we need ( should not be null due to require component so no need to check )
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.updateRotation = false;
        agent.updatePosition = true;
    }


    private void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance)
            Vector3.MoveTowards(this.transform.position, target.position, 10f);

    }


    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
