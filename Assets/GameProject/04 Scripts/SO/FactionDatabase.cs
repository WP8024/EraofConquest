using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionDatabase : MonoBehaviour
{
    //�Ѽǵ����ͺ��̽��� �ϳ��� ���� �̱������� ��𼭵� instance�� ����
    public static FactionDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Faction> factionDB = new List<Faction>();
}
