using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum KeyAction { UP,DOWN,LEFT,RIGHT,KEYCOUNT }

public static class KeySetting { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class InputManager : MonoBehaviour
{
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };


    private void Start()
    {

        for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
        {
            KeySetting.keys.Add((KeyAction)i, defaultKeys[i]);
        }
        
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


