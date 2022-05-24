using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyAction { UP,DOWN,LEFT,RIGHT,KEYCOUNT }

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class InputManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };

    #region 유닛관련 클릭처리
    [SerializeField]
    private LayerMask layerUnit;
    private Camera mainCamera;
    private UnitController unitController;
    private Node node;


    #endregion
    private void Start()
    {
        mainCamera = Camera.main;
        unitController = GetComponent<UnitController>();

        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }

    }
    private void Update()
    {

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
            //노드 클릭시 건설모드에만 활성화하도록 수정
            //node.OnClickNode();

            #region rayhit부분만 받아와 쓰는거 작성중
            //if (hit.transform.GetComponent<Unit>() == null) return;
            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    unitController.ShiftClickSelectUnit(hit.transform.GetComponent<Unit>());
            //}
            //else
            //{
            //    unitController.ClickSelectUnit(hit.transform.GetComponent<Unit>());

            //}

            #endregion
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
    private void OnMouseDown()
    {
        //input매니저에 들어가야할듯함
        offset = transform.position - GetMouseWorldPosition();
    }
    private void OnMouseDrag()
    {
        Vector3 pos = GetMouseWorldPosition() + offset;
    
    }
}


