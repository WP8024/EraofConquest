using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyAction { UP,DOWN,LEFT,RIGHT,KEYCOUNT }

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class InputManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };
    [SerializeField]
    private RectTransform dragRectangle;
    private Rect dragRect;
    private Vector2 start = Vector2.zero;
    private Vector2 end = Vector2.zero;


    #region 유닛관련 클릭처리
    [SerializeField]
    private LayerMask layerUnit;
    [SerializeField]
    private LayerMask layerGround;
    private Camera mainCamera;
    private UnitController unitController;
    private Node node;

    public void Awake()
    {
        mainCamera = Camera.main;
        unitController = GetComponent<UnitController>();

        DrawDragRectangle();
    }

    #endregion
    private void Start()
    {
  
        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }

    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
              RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerGround))
            {
                UnitController.Instance.MoveSelectedUnits(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray,out hit, Mathf.Infinity, layerUnit)) {
                {
                    if (hit.transform.GetComponent<Unit>() == null) return;
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        unitController.ShiftClickSelectUnit(hit.transform.GetComponent<Unit>());
                    }
                    else
                    {
                        unitController.ClickSelectUnit(hit.transform.GetComponent<Unit>());
                        
                    }
                }
            }
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    unitController.DeselectAll();
                }
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            start = Input.mousePosition;
            dragRect = new Rect();
        }
        if (Input.GetMouseButton(0))
        {
            end = Input.mousePosition;
            DrawDragRectangle();
        }
        if (Input.GetMouseButtonUp(0))
        {
            CalculateDragRect();
            SelectUnits();

            start = end = Vector2.zero;
            DrawDragRectangle();
        }
    }
    private void DrawDragRectangle()
    {
        dragRectangle.position = (start + end) * 0.5f;
        dragRectangle.sizeDelta = new Vector2(Mathf.Abs(start.x - end.x), Mathf.Abs(start.y - end.y));
    }
    private void CalculateDragRect()
    {
        if (Input.mousePosition.x < start.x)
        {
            dragRect.xMin = Input.mousePosition.x;
            dragRect.xMax = start.x;
        }
        else
        {
            dragRect.x = start.x;
            dragRect.xMax = Input.mousePosition.x;
        }

        if (Input.mousePosition.y < start.y)
        {
            dragRect.yMin = Input.mousePosition.y;
            dragRect.yMax = start.y;
        }
        else
        {
            dragRect.yMin = start.y;
            dragRect.yMax = Input.mousePosition.y;
        }
    }

    private void SelectUnits()
    {
        foreach(Unit unit in unitController.UnitList)
        {
                if (dragRect.Contains(mainCamera.WorldToScreenPoint(unit.transform.position)))
                {

                    unitController.DragSelectUnit(unit);
                }
            
        }
    }

    public RaycastHit GetRayHitObject()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray, out hit, Mathf.Infinity, layerUnit);
        return hit;
    }


    private Vector3 offset;
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    //private void OnMouseDown()
    //{
    //    //input매니저에 들어가야할듯함
    //    offset = transform.position - GetMouseWorldPosition();
    //}
    //private void OnMouseDrag()
    //{
    //    Vector3 pos = GetMouseWorldPosition() + offset;
    
    //}
}


