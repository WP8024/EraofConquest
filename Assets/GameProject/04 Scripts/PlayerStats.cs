using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
	public static PlayerStats instance = null;

	public static int Money { get; set; }
	public int startMoney = 400;

	public static int MaxUnit { get; set; }

	public static int MaxBuilding { get; set; }


	public int curUnit;
	public int curBuilding;
	public int up_MaxUnit;
	public int up_MaxBuilding;
	public int up_MeleeUnit;
	public int up_RangeUnit;
	public int up_MagicUnit;
	public int up_CavalryUnit;

    private void Awake()
    {
		if (instance != null)
			instance = this;
	}

    void Start()
	{
		Money = startMoney;
	}

	public void Income(int income)
    {
		Money += income;
    }
	public void spendMoney(int cost)
    {
		Money -= cost;
    }



}
