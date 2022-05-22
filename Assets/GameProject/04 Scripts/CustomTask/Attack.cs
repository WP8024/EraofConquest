using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{
    Unit unit;
    ObjectBody target;
    public SharedGameObject targetGameObject;
    public SharedString stateName;

    private Animator animator;
    private GameObject prevGameObject;
    public override void OnAwake()
    {
        target = targetGameObject.Value.GetComponent<ObjectBody>();
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

        unit.Attack(target);
        return TaskStatus.Success;

    }
}