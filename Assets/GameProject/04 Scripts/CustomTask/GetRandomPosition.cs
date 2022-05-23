using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;


[TaskCategory("Custom")]
public class GetRandomPosition : Conditional
{
    Unit unit;
    SharedTransform target;
    Vector3 randomPos;
    NavMeshHit hit;
    public override void OnAwake()
    {
        unit = GetComponent<Unit>();
     
    }

    public override void OnStart()
    {


    }

    public override TaskStatus OnUpdate()
    {
        randomPos = transform.position + Random.insideUnitSphere * unit.searchRange;
 
        if (NavMesh.SamplePosition(randomPos, out hit, 1.0f, NavMesh.AllAreas))
        {
            target.Value.position = hit.position;
            return TaskStatus.Success;
        }


        return TaskStatus.Running;
    }
}
