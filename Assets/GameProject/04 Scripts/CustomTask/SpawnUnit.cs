using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
public class SpawnUnit : Action
{
    Building bd;
    PlayerStats player;
    public override void OnAwake()
    {
     
        bd = GetComponent<Building>();
    }
    

    public override void OnBehaviorComplete()
    {
      
        if (bd.isSpawner)
        {
            if (bd.createUnit())
            {
                
            }
        }
    }
}
