using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//저장가능한 형태로 만들기위해
[System.Serializable]
public class UnitData
{
    public int unitid;
    public string name;
    public int price;
    public float maxhp;
    public float damage;
    public float attackdistance;
    public float searchrange;
    public float movespeed;

}
