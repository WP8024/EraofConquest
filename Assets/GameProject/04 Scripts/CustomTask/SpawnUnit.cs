using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
public class SpawnUnit : Action
{
    Building bd;
    PlayerData player;
    public override void OnAwake()
    {
     
        bd = GetComponent<Building>();
        player = DataManager.instance.player;
    }
    

    public override void OnBehaviorComplete()
    {
      
        if (bd.isSpawner)
        {
            if (bd.createUnit())
            {
                return;
            }
        }
    }
}
