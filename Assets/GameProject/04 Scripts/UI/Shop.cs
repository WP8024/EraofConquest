using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public BuildingBlueprint[] building;

    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectBuilding(int key)
    {
        Debug.Log("Select building : " + building[key]);
        buildManager.SelectTurretToBuild(building[key]);
    }

    public void SelectUpgrade(int key)
    {

    }


}
