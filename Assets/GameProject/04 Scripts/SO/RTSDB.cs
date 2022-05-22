using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class RTSDB : ScriptableObject
{
	//���� �����Ͱ�
	public List<UnitData> UnitDB; 
	//�ǹ� �����Ͱ�
	public List<BuildingData> BuildingDB; 
	//���Ǵ� ���̾�α׸��
	public List<DialogData> DialogDB;
	//�÷��̾� �� �� ������
	public List<PlayerData> PlayerDB;
}
