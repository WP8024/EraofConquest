using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{


    public List<Unit> selectedUnitList;

    private void Awake()
    {
        selectedUnitList = new List<Unit>();
    }


    public void ClickSelectUnit(Unit newUnit)
    {
        DeselectAll();

        SelectUnit(newUnit);
    }
    public void DragSelectUnit(Unit newUnit)
    {
        // ���ο� ������ ����������
        if (!selectedUnitList.Contains(newUnit))
        {
            SelectUnit(newUnit);
        }
    }
    public void DeselectAll()
    {
        for (int i = 0; i < selectedUnitList.Count; ++i)
        {
            selectedUnitList[i].DeselectObject();
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
        newUnit.DeselectObject();
        // ������ ���� ������ ����Ʈ�� ����
        selectedUnitList.Add(newUnit);
    }

}
