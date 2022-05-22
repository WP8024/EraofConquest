using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
	//public static PlayerStats instance = null;

	public static int Money { get; set; }
	public static int MaxUnit { get; set; }
	public static int MaxBuilding { get; set; }

	public int startMoney = 400;

	public int curUnit=0;
	public int curBuilding=0;
	public int up_MaxUnit=1;
	public int up_MaxBuilding=1;
	public int up_MeleeUnit=1;
	public int up_RangeUnit=1;
	public int up_MagicUnit=1;
	public int up_CavalryUnit=1;

    private void Awake()
    {
		//if (instance != null)
		//	instance = this;
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
