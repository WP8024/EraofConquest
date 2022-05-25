using System.Collections;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SetUpGameObject : Action
{


    public SharedGameObject curGameObject;
    public SharedString targetTag;
    public SharedBool isSetup;
    public SharedString curTag;


    public override void OnAwake()
    {

        curGameObject.Value = gameObject;
        if (curGameObject.Value.CompareTag("Blue"))
        {
            targetTag.Value = "Red";
            curTag.Value = "Blue";
        }
        else
        {
            targetTag.Value = "Blue";
            curTag.Value = "Red";
        }
    }

    public override TaskStatus OnUpdate()
    {
        curGameObject.Value = gameObject;

        targetTag.Value = curGameObject.Value.CompareTag("Blue") ? "Red" : "Blue";

        isSetup.Value = true;
        return TaskStatus.Success;
    }
}
