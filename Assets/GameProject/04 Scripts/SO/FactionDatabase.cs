using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionDatabase : MonoBehaviour
{
    //팩션데이터베이스는 하나만 존재 싱글톤적용 어디서든 instance로 접근
    public static FactionDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Faction> factionDB = new List<Faction>();
}
