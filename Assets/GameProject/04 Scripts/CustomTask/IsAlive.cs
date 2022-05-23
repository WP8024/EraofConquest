using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
public class IsAlive : Conditional
{
    public SharedTransform target;
    private ObjectBody objbody;
    public override void OnAwake()
    {

    }


    public override void OnStart()
    {
        // the target may be null if it has been destoryed. In that case set the health to null and return
        if (target.Value == null)
        {
            objbody = null;
        }
        // cache the health component
        objbody = target.Value.GetComponent<ObjectBody>();
    }

    // OnUpdate will return success if the object is still alive and failure if it not
    public override TaskStatus OnUpdate()
    {
        if (target == null)
        {
            return TaskStatus.Failure;
        }
        else if (objbody != null && objbody.health > 0)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Success;
    }
}