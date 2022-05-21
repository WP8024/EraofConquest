using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class RTSDB : ScriptableObject
{
	public List<UnitData> UnitDB; // Replace 'EntityType' to an actual type that is serializable.
	public List<BuildingData> BuildingDB; // Replace 'EntityType' to an actual type that is serializable.
	public List<DialogData> DialogDB; // Replace 'EntityType' to an actual type that is serializable.
}
