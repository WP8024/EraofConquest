using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
public class IsRanged : Conditional
{
    public SharedGameObject curObject;

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (curObject.Value.GetComponent<Unit>().projectilePrefab != null)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Failure;
    }
}
