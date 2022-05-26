using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    private static UnitController instance;
    public static UnitController Instance
    {
        get
        {
            var obj = FindObjectOfType<UnitController>();
            if (obj != null)
            {
                instance = obj;
            }
            else
            {
                var newObj = new GameObject().AddComponent<UnitController>();
                instance = newObj;
            }
            return instance;
        }
    }

    public List<Unit> selectedUnitList;
    public List<Unit> UnitList { private set; get; }

    private void Awake()
    {
        selectedUnitList = new List<Unit>();
        UnitList = new List<Unit>();

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

    }

    public void ClickSelectUnit(Unit newUnit)
    {
        DeselectAll();

        SelectUnit(newUnit);
    }

    public void ShiftClickSelectUnit(Unit newUnit)
    {
        if (selectedUnitList.Contains(newUnit))
        {
            DeselectUnit(newUnit);
        }
        // ���ο� ������ ����������
        if (!selectedUnitList.Contains(newUnit))
        {
            SelectUnit(newUnit);
        }
    }

    public void DragSelectUnit(Unit newUnit)
    {
        if (!selectedUnitList.Contains(newUnit))
        {
            SelectUnit(newUnit);
        }
    }
    public void DeselectAll()
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].DeselectUnit();
        }

        selectedUnitList.Clear();
    }
    public void MoveSelectedUnits(Vector3 pos)
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].SelectUnitMove(pos);
        }
    }
    
    private void SelectUnit(Unit newUnit)
    {
        // ������ ���õǾ��� �� ȣ���ϴ� �޼ҵ�
        newUnit.SelectUnit();
        // ������ ���� ������ ����Ʈ�� ����
        selectedUnitList.Add(newUnit);
    }


    private void DeselectUnit(Unit newUnit)
    {
        newUnit.DeselectUnit();
        selectedUnitList.Remove(newUnit);
    }


}
