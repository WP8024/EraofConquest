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
	//BuildManager buildManager;
	//DataManager dataManager;
	void Start()
	{
		rend = GetComponent<Renderer>();
		startColor = rend.material.color;
		
		//buildManager = BuildManager.instance;
		//dataManager = DataManager.Instance;
	}

	public Vector3 GetBuildPosition()
	{
		return transform.position + positionOffset;
	}


	//public void OnClickNode()
 //   {
	//	if (EventSystem.current.IsPointerOverGameObject())
	//		return;

	//	if (building != null)
	//	{
	//		BuildManager.Instance.SelectNode(this);
	//		return;
	//	}

	//	if (!BuildManager.Instance.CanBuild)
	//		return;

	//	BuildTurret(BuildManager.Instance.GetTurretToBuild());
	//	building = null;
	//}

	void BuildTurret(BuildingBlueprint blueprint)
	{
		if (DataManager.Instance.player.money < blueprint.cost)
		{
			Debug.Log(blueprint.prefab);
			Debug.Log("Not enough money to build that!");
			return;
		}

		DataManager.Instance.player.money -= blueprint.cost;

		GameObject _building = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		building = _building;

		buildingBlueprint = blueprint;

		GameObject effect = (GameObject)Instantiate(BuildManager.Instance.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		Debug.Log("Construct build!");
	}

	public void UpgradeBuilding()
	{
		if (DataManager.Instance.player.money < buildingBlueprint.upgradeCost)
		{
			Debug.Log("Not enough money to upgrade that!");
			return;
		}

		DataManager.Instance.player.money -= buildingBlueprint.upgradeCost;

		//Get rid of the old turret
		Destroy(building);

		//Build a new one
		GameObject _building = (GameObject)Instantiate(buildingBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
		building = _building;

		GameObject effect = (GameObject)Instantiate(BuildManager.Instance.buildEffect, GetBuildPosition(), Quaternion.identity);
		Destroy(effect, 5f);

		isUpgraded = true;

		Debug.Log("Turret upgraded!");
	}

	public void SellBuilding()
	{

		DataManager.Instance.player.money += buildingBlueprint.GetSellAmount();

		GameObject effect = (GameObject)Instantiate(BuildManager.Instance.sellEffect, GetBuildPosition(), Quaternion.identity);
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
            BuildManager.Instance.SelectNode(this);
            return;
        }

        if (!BuildManager.Instance.CanBuild)
            return;
        //ÅÍ·¿Áþ°í ÃÊ±âÈ­
        BuildTurret(BuildManager.Instance.GetTurretToBuild());
        building = null;
    }

    //void OnMouseEnter()
    //{
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return;

    //    if (!BuildManager.Instance.CanBuild)
    //        return;

    //    if (BuildManager.Instance.HasMoney)
    //    {
    //        rend.material.color = hoverColor;
    //    }
    //    else
    //    {
    //        rend.material.color = notEnoughMoneyColor;
    //    }
    //}

    //void OnMouseExit()
    //{
    //    rend.material.color = startColor;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (changed) { return; }
    //    if (other.tag == "Blue")
    //    {
    //        rend.material.color = blue;
    //    }
    //    else if (other.tag == "Red")
    //    {
    //        rend.material.color = red;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    rend.material.color = startColor;
    //}
}
