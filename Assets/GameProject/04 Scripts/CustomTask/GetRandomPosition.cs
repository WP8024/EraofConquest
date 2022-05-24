using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;



[TaskCategory("Custom")]
public class GetRandomPosition : Conditional
{
    Unit unit;
    public SharedTransform target;
    public Vector3 randomPos;
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
