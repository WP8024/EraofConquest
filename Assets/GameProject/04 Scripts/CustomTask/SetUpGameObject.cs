using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetUpGameObject : Action
{


    public SharedGameObject curGameObject;
    public SharedString targetTag;
    public SharedBool isSetup;


    public override void OnAwake()
    {

        curGameObject.Value = gameObject;
        if (curGameObject.Value.CompareTag("Blue"))
        {
            targetTag.Value = "Red";
        }
        else
        {
            targetTag.Value = "Blue";
        }
    }

    public override TaskStatus OnUpdate()
    {
        curGameObject.Value = gameObject;

        targetTag.Value = curGameObject.Value.CompareTag("Blue") ? "Red" : "Blue";

        return TaskStatus.Success;
    }
}
