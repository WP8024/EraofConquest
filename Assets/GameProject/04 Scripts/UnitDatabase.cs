using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "UnitDB",menuName ="Scriptable Object/Unit DB",order = int.MaxValue)]
public class UnitDatabase : MonoBehaviour
{
    public static UnitDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public List<UnitData> unitDB = new List<UnitData>();
}
