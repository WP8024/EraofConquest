using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{

	public Color hoverColor;
	public Color notEnoughMoneyColor;
	public Color blue, red;
	public Vector3 positionOffset;

	public bool structmode = false;

	[HideInInspector]
	public GameObject building;
	[HideInInspector]
	public BuildingBlueprint buildingBlueprint;
	[HideInInspector]
	public bool isUpgraded = false;

	private Renderer rend;
	private Color startColor;
	public bool changed = false;
	BuildManager buildManager;
	DataManager dataManager;
	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		
		buildManager = BuildManager.instance;
		dataManager = DataManager.instance;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}


	public void OnClickNode()
    {
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		if (building != null)
		{
			buildManager.SelectNode(this);
			return;
		}

		if (!buildManager.CanBuild)
			return;

		BuildTurret(buildManager.GetTurretToBuild());
		building = null;
	}

	void BuildTurret(BuildingBlueprint blueprint)
	{
		if (dataManager.player.money < blueprint.cost)
		{
			Debug.Log(blueprint.prefab);
			Debug.Log("Not enough money to build that!");
			return;
		}

		dataManager.player.money -= blueprint.cost;

		GameObject _building = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		building = _building;

		buildingBlueprint = blueprint;

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Debug.Log("Turret build!");
	}

	public void UpgradeBuilding()
	{
		if (dataManager.player.money < buildingBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		dataManager.player.money -= buildingBlueprint.upgradeCost;

		//Get rid of the old turret
		Destroy(building);

		//Build a new one
		GameObject _building = (GameObject)Instantiate(buildingBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		building = _building;

		GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;

		Debug.Log("Turret upgraded!");
	}

	public void SellBuilding()
	{

		dataManager.player.money += buildingBlueprint.GetSellAmount();

		GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Destroy(building);
		buildingBlueprint = null;
	}


    void OnMouseDown()
    {

        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (building != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
        building = null;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (changed) { return; }
        if (other.tag == "Blue")
        {
            rend.material.color = blue;
        }
        else if (other.tag == "Red")
        {
            rend.material.color = red;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        rend.material.color = startColor;
    }
}
