using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
public class Attack : Action
{


    Unit unit;
    ObjectBody objbody;
    public SharedGameObject targetGameObject;
    public SharedString stateName;
    public SharedTransform target;

    private Animator animator;
    private GameObject prevGameObject;
    public override void OnAwake()
    {
        unit = GetComponent<Unit>();
        target = targetGameObject.Value.GetComponent<Transform>();
        objbody = target.Value.GetComponent<ObjectBody>();
    }

    public override void OnStart()
    {
        //target = targetGameObject.Value.GetComponent<Transform>();
        var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
        if (currentGameObject != prevGameObject)
        {
            animator = currentGameObject.GetComponent<Animator>();
            prevGameObject = currentGameObject;
        }
    }

    public override TaskStatus OnUpdate()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator is null");
            return TaskStatus.Failure;
        }

        unit.Attack(objbody);
        return TaskStatus.Success;

    }
}