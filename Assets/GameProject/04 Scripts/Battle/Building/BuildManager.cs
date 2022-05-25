using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
	private static BuildManager instance;
	public static BuildManager Instance
    {

		get
		{
			var obj = FindObjectOfType<BuildManager>();
			if (obj != null)
			{
				instance = obj;
			}
			else
			{
				var newObj = new GameObject().AddComponent<BuildManager>();
				instance = newObj;
			}
			return instance;
		}

	}

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(instance.gameObject);
		}
		//SetUP();
		//씬이 바뀌어도 사라지면 안되기때문에 이동
	}

	public GameObject buildEffect;
	public GameObject sellEffect;

	private BuildingBlueprint Building;
	private Node selectedNode;

	public NodeUI nodeUI;

	public bool CanBuild { get { return Building != null; } }
	public bool HasMoney { get { return DataManager.Instance.player.money >= Building.cost; } }

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
