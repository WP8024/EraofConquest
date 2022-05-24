using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyAction { UP,DOWN,LEFT,RIGHT,KEYCOUNT }

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class InputManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };

    #region ���ְ��� Ŭ��ó��
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
            //��� Ŭ���� �Ǽ���忡�� Ȱ��ȭ�ϵ��� ����
            //node.OnClickNode();

            #region rayhit�κи� �޾ƿ� ���°� �ۼ���
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
        //input�Ŵ����� �����ҵ���
        offset = transform.position - GetMouseWorldPosition();
    }
    private void OnMouseDrag()
    {
        Vector3 pos = GetMouseWorldPosition() + offset;
    
    }
}


