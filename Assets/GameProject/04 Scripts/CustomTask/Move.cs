using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
public class Move : Action
{
    Unit unit;
    NavMeshAgent agent;
    public SharedGameObject targetGameObject;
    public SharedString stateName;
    public SharedTransform target;
    public override void OnStart()
    {
        agent = transform.GetComponent<NavMeshAgent>();
    }

    public override TaskStatus OnUpdate()
    {
        if (!agent.hasPath)
        {
            return TaskStatus.Failure;
        }

        if (target != null)
        {
            agent.SetDestination(targetGameObject.Value.transform.position);
            ///move character towards target tile position
            //transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
            /////rotate character
            //transform.rotation = Quaternion.Lerp(this.transform.rotation, moveRotation, 0.1f);
        }
        else if (target == null)
        {
            agent.SetDestination(target.Value.position);
            
        }
        if (agent.remainingDistance < agent.stoppingDistance)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
