using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hit;
    public GameObject flash;
    public GameObject[] Detached;

    public LayerMask collisionMask;
    public float speed = 1;
    public Vector3 rotationOffset = new Vector3(0, 0, 0);
    public float damage = 10;
    float lifetime = 2;
    public float hitOffset = 0f;

    private ObjectBody target;

    void start()
    {
       
        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if(flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
            
        }
        transform.LookAt(target.transform);
        Destroy(gameObject, lifetime);

    }



    private bool isMoving = false;

    /// 발사체생성시 호출 Called when projectile created
    public void Init(ObjectBody _target)
    {
        target = _target;

        isMoving = true;

        if (flash != null)
        {
            var flashInstance = Instantiate(flash, transform.position, Quaternion.identity);
            flashInstance.transform.forward = gameObject.transform.forward;
            var flashPs = flashInstance.GetComponent<ParticleSystem>();
            if (flashPs != null)
            {
                Destroy(flashInstance, flashPs.main.duration);
            }
            else
            {
                var flashPsParts = flashInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(flashInstance, flashPsParts.main.duration);
            }
        }

    }

    public void SetTarget(Transform _target)
    {
        target = _target.GetComponent<ObjectBody>();
    }

    public void SetSpeed(float newspeed)
    {
        speed = newspeed;
    }

    private void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            float moveDistance = speed;
            CheckCollisions(moveDistance);
            transform.LookAt(target.transform);
            transform.Translate(Vector3.forward * moveDistance);
        }
    }
    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        if (hit.transform.CompareTag(target.tag))
        {
            IDamageable damagealbeObject = hit.collider.GetComponent<IDamageable>();
            if (damagealbeObject != null)
            {
                damagealbeObject.TakeHit(damage, hit);
            }
            print(hit.collider.gameObject.name);
            
        }
        Destroy(gameObject);
    }

    void OnHitObject(Collider c)
    {
        if (c.tag == target.tag)
        {

            IDamageable damagealbeObject = c.GetComponent<IDamageable>();
            if (damagealbeObject != null)
            {
                damagealbeObject.TakeDamage(damage);
            }
            //print(hit.collider.gameObject.name);
           
        }
        Destroy(gameObject);
    }



    //https ://docs.unity3d.com/ScriptReference/Rigidbody.OnCollisionEnter.html
    //void OnCollisionEnter(Collision collision)
    //{
    //    //Lock all axes movement and rotation
    //    speed = 0;

    //    ContactPoint contact = collision.contacts[0];
    //    Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
    //    Vector3 pos = contact.point + contact.normal * hitOffset;

    //    if (hit != null)
    //    {
    //        var hitInstance = Instantiate(hit, pos, rot);
    //        if (rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(rotationOffset); }
    //        else { hitInstance.transform.LookAt(contact.point + contact.normal); }

    //        var hitPs = hitInstance.GetComponent<ParticleSystem>();
    //        if (hitPs != null)
    //        {
    //            Destroy(hitInstance, hitPs.main.duration);
    //        }
    //        else
    //        {
    //            var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
    //            Destroy(hitInstance, hitPsParts.main.duration);
    //        }
    //    }
    //    foreach (var detachedPrefab in Detached)
    //    {
    //        if (detachedPrefab != null)
    //        {
    //            detachedPrefab.transform.parent = null;
    //        }
    //    }
    //    Destroy(gameObject);
    //}



    #region 유도탄예제
    //public Transform playerTr;
    //public Rigidbody ballrigid;
    //public float turn;
    //public float ballVelocity;

    //public void FixedUpdate() //유도
    //{
    //    ballrigid.velocity = transform.forward * ballVelocity;
    //    var ballTargetRotation = Quaternion.LookRotation(playerTr.position + new Vector3(0, 0.8f) - transform.position);
    //    ballrigid.MoveRotation(Quaternion.RotateTowards(transform.rotation, ballTargetRotation, turn));
    //}
    #endregion
}
