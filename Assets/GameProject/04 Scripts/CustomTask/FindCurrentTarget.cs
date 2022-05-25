using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
[TaskCategory("Custom")]
public class FindCurrentTarget :Action
{
    public SharedTransform target;
    public SharedString targetTag;
    public float distance;
    private Transform targetTransform;


    public override TaskStatus OnUpdate()
    {

        if (target != null) return TaskStatus.Running;

        //targetTag = target.Value.tag;
        //targetTag = (transform.tag == "Blue") ? "Red" : "Blue";
        targetTransform = GameObject.FindGameObjectWithTag(targetTag.Value).transform;
        target = targetTransform;
        if (target == null) return TaskStatus.Failure;
       
        return TaskStatus.Success;
    }
}
