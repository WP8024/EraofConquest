using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;


[TaskCategory("Custom")]
public class Seek : Action
{
    SharedTransform target;
    NavMeshAgent agent;
    public override void OnStart()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        var curpos = transform.position;
        curpos.y = agent.destination.y;
        if(Vector3.SqrMagnitude(curpos-agent.destination)< agent.stoppingDistance)
        {
            curpos = target.Value.position;
        }
        agent.SetDestination(target.Value.position);

        return TaskStatus.Running;
    }
}
