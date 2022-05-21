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
        // 새로운 유닛을 선택했으면
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
        // 유닛이 선택되었을 때 호출하는 메소드
        newUnit.DeselectObject();
        // 선택한 유닛 정보를 리스트에 저장
        selectedUnitList.Add(newUnit);
    }

}
