using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance;

    [SerializeField]
    private LayerMask layermask;


    [SerializeField]
    private GameObject unitMarker;

    [SerializeField]
    GameObject currentlySelected;

   


    RaycastHit hit;


    public void SetSelected(GameObject toSet)//setter for selected object
    {
        currentlySelected = toSet;
        currentlySelected.GetComponent<ObjectBody>().OnSelect(); //현재 시연 목적으로, 나중에 일부 기능을 추가할 수 있습니다.
    }

    public GameObject GetSelected()//getter for selected object;
    {
        return currentlySelected;
    }

    void ClearSelected()
    {
        currentlySelected = null;
    }

    void CheckForLeftMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicking, Looking for ray cast hits");
            SelectionRaycast();
        }
    }

    void SelectionRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100f, layermask)) 
        {
            GameObject hitObject = hit.collider.gameObject;

            Debug.Log(hitObject.name);
            SetSelected(hitObject);
        }
    }


}
