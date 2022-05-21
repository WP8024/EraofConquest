using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

public class FindCurrentTarget :Action
{
    public SharedTransform target;
    public string targetTag;
    public float distance;
    private Transform targetTransform;


    public override TaskStatus OnUpdate()
    {

        if (target != null) return TaskStatus.Running;


        targetTag = (transform.tag == "Blue") ? "Red" : "Blue";
        targetTransform = GameObject.FindGameObjectWithTag(targetTag).transform;
        target = targetTransform;
        return TaskStatus.Success;


    }
}
