using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class RTSDB : ScriptableObject
{
	//유닛 데이터값
	public List<UnitData> UnitDB; 
	//건물 데이터값
	public List<BuildingData> BuildingDB; 
	//사용되는 다이어로그목록
	public List<DialogData> DialogDB;
	//플레이어 및 적 데이터
	public List<PlayerData> PlayerDB;
}
