using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    private List<UnitData> unitDatas;

    private GameObject unitPrefab;
    // Use this for initialization
    void Start()
    {

    }

    //public Unit spawnUnit(UnitData id)
    //{
    //    var newUnit = Instantiate(unitPrefab).GetComponent<Unit>();
    //    newUnit.unitData = unitDatas[id];

    //}
}