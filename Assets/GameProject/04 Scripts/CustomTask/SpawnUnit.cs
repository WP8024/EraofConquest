using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;


[TaskCategory("Custom")]
public class SpawnUnit : Action
{
    public SharedGameObject curGameObject;
    Building bd;
    //PlayerData player;
    public override void OnAwake()
    {

        bd = curGameObject.Value.GetComponent<Building>();
        //player = DataManager.Instance.player;
    }

    public override TaskStatus OnUpdate()
    {
        if (!bd.isSpawner)
        {
            return TaskStatus.Failure;
        }
        else
        {
            bd.createUnit();
            return TaskStatus.Success;
        }


    }
}

