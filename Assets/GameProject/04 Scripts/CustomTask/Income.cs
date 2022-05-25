using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

[TaskCategory("Custom")]
public class Income : Action
{
    public SharedGameObject curObject;
    public SharedString targetTag;
    private Building building;
    // Start is called before the first frame update

    public override  void OnAwake()
    {
       building = curObject.Value.GetComponent<Building>();
        
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (building.turngold == 0)
        {
            return TaskStatus.Failure;
        }
        if (curObject.Value.CompareTag("Red"))
        {
            DataManager.Instance.enemy.money += building.turngold;
            return TaskStatus.Success;
        }
        else
        {
            DataManager.Instance.player.money += building.turngold;
            return TaskStatus.Success;
        }
    }
}
