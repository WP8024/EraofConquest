using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public abstract class ObjectBody : MonoBehaviour , IDamageable
{
    // 정적 변수이기 때문에 1개만 존재
    //public ObjectInfo objinfo;
    //public GameObject objModel;

    public RTSDB DB;

    public enum Faction { Blue, Red, Yellow, Green, None };
    public enum ObjType { Unit, Building, None }
    public enum AttackType { Melee, Ranged }

    [Header("Object Stat")]
    public Faction faction;
    public float startHealth;
    public float health;
    public int price;
    //public float attackDamage;
    public AudioClip SpawnSound;
    public AudioClip destroySound;

    public bool isDead;
 
    public GameObject selectedObject;
    public Material[] materials;

    public ParticleSystem destroyParticle;
    private AudioSource audioSource;

    [SerializeField]
    public GameObject selectedMarker;
    public bool selected;

    public virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        destroyParticle = GetComponent<ParticleSystem>();
    }
    public virtual void Start()
    {

        health = startHealth;
        isDead = false;
        selected = false;
        if (destroyParticle != null)
        {

        }
        if (SpawnSound != null)
        {
            audioSource.PlayOneShot(SpawnSound);
        }
    }


    public virtual void Setup(string name)
    {
 
    }
    public virtual void Setup(ObjectBody obj,int id)
    {

    }
    // GameController 클래스에서 모든 에이전트의 Updated()를 호출해 에이전트를 구동한다.
    public abstract void Updated();


    public virtual void TakeHit(float damage,RaycastHit raycast)
    {
        TakeDamage(damage);
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
        
        GameObject.Destroy(gameObject);
    }


    public virtual void OnSelect()
    {
        Debug.Log("Root Class" + this.gameObject.transform.position);
    }
    public virtual void SelectObject()
    {
        selected = true;
        selectedMarker.SetActive(selected);
    }
    public virtual void DeselectObject()
    {
        selected = false;
        selectedMarker.SetActive(selected);
    }

    // 메시지를 발송했을 때 수신하는 메소드 (MessageDispatcher 클래스에서 호출)
    public virtual bool HandleMessage(Telegram telegram)
    {
        Debug.Log("not resiver ObjectBody.Cs");
        return false;
    }
}
