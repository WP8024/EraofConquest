using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
	public GameObject ui;

	public TextMeshProUGUI upgradeCost;
	public Button upgradeButton;

	public TextMeshProUGUI sellAmount;

	private Node target;

	public void SetTarget(Node _target)
	{
		target = _target;

		transform.position = target.GetBuildPosition();

		if (!target.isUpgraded)
		{
			upgradeCost.text = "$" + target.buildingBlueprint.upgradeCost;
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeCost.text = "DONE";
			upgradeButton.interactable = false;
		}

		sellAmount.text = "$" + target.buildingBlueprint.GetSellAmount();

		ui.SetActive(true);
	}

	public void Hide()
	{
		ui.SetActive(false);
	}

	public void Upgrade()
	{
		target.UpgradeBuilding();
		BuildManager.instance.DeselectNode();
	}

	public void Sell()
	{
		target.SellBuilding();
		BuildManager.instance.DeselectNode();
	}

}
