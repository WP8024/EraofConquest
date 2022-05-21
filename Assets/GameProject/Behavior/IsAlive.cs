using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace BehaviorDesigner.Custom
{
    [TaskCategory("Custom")]
    public class IsAlive : Conditional
    {
        [Tooltip("The target object that we are interested in to determine if it is still alive")]
        public SharedTransform target;
        private ObjectBody objbody;
        public override void OnAwake()
        {

        }


        public override void OnStart()
        {
            // the target may be null if it has been destoryed. In that case set the health to null and return
            if (target.Value == null)
            {
                objbody = null;
            }
            // cache the health component
            objbody = target.Value.GetComponent<ObjectBody>();
        }

        // OnUpdate will return success if the object is still alive and failure if it not
        public override TaskStatus OnUpdate()
        {
            if (target == null)
            {
                return TaskStatus.Failure;
            }
            else if(objbody != null && objbody.health > 0)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Success;
        }
    }
}