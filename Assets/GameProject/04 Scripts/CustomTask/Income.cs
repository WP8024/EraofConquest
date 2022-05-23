using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Custom")]
public class Income : Action
{
    DataManager datamanaer;
    private Building building;
    // Start is called before the first frame update

    public override  void OnAwake()
    {
        building = gameObject.GetComponent<Building>();
        
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        datamanaer.player.money += building.turngold;
        return TaskStatus.Success;
    }
}
