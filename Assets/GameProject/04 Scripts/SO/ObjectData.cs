using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//¿Ø¥÷,∞«π∞ «¡∏Æ∆’»≠
[System.Serializable]
public class ObjectData
{
    [Header("Enum")]
    public ObjectBody.Faction faction;
    public ObjectBody.ObjType objType;
    public ObjectBody.AttackType attackType;
   
    [Header("modelid")]
    public int objid;

    public GameObject bluePrefab, redPrefab;
    public AudioClip deathClip, attackClip;
    public ParticleSystem deathParticle,spawnParticle;

}
