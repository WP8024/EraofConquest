using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	public static BuildManager instance;

	void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("More than one BuildManager in scene!");
			return;
		}
		instance = this;
	}

	public GameObject buildEffect;
	public GameObject sellEffect;

	private BuildingBlueprint Building;
	private Node selectedNode;

	public NodeUI nodeUI;

	public bool CanBuild { get { return Building != null; } }
	public bool HasMoney { get { return DataManager.instance.player.money >= Building.cost; } }

	public void SelectNode(Node node)
	{
		if (selectedNode == node)
		{
			DeselectNode();
			return;
		}

		selectedNode = node;
		Building = null;

		nodeUI.SetTarget(node);
	}

	public void DeselectNode()
	{
		selectedNode = null;
		nodeUI.Hide();
	}

	public void SelectTurretToBuild(BuildingBlueprint turret)
	{
		Debug.Log("build"+turret);
		Building = turret;
		DeselectNode();
	}
	public void DeSelectBuild()
    {
		Building = null;

    }

	public BuildingBlueprint GetTurretToBuild()
	{
		return Building;
	}

}
