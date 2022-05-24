using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Custom")]
public class IsSetup : Conditional
{
    public SharedBool isSetup;


    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (!isSetup.Value)
        {
           return TaskStatus.Success;
        }
        else return TaskStatus.Failure;
    }
}
