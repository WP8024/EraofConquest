using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory ("Custom")]
public class FireProjectile : Action
{
    public SharedGameObject curObject;
    public Projectile projectilePrefab;
    public SharedTransform target;
    // Use this for initialization

    private Collider col;
    public override void OnAwake()
    {
       projectilePrefab = curObject.Value.GetComponent<Building>().projectilePrefab;
        col = GetComponent<Collider>();
    }

    // OnUpdate will return success in one frame after it has created the projectile
    public override TaskStatus OnUpdate()
    {
        var spawnedProjectile = Object.Instantiate(projectilePrefab);
        spawnedProjectile.transform.position = transform.position;
        spawnedProjectile.transform.LookAt(target.Value);
        spawnedProjectile.transform.parent = transform;
        
        var projectile = spawnedProjectile.GetComponent<Projectile>();
        projectile.SetTarget(target.Value);

        Physics.IgnoreCollision(col, projectile.GetComponent<Collider>());
        return TaskStatus.Success;
    }
}
