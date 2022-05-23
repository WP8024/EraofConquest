using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetTarget : Action
{
    public Transform target;
    public string tag;
    public SharedTransform targetVariable = null;

    public override void OnAwake()
    {
        if (tag.Length > 0)
        {
            target = GameObject.FindGameObjectWithTag(tag).transform;
        }
    }

    public override TaskStatus OnUpdate()
    {
        targetVariable.Value = target;

        return TaskStatus.Success;
    }
}
