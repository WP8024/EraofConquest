using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Faction 
{
    public FactionType factionType;
    public string factionName;
    public Material factionMaterial;
    public Texture factionTexture;

    public int currentGold;
    public int MaxUnit;
    public int MaxBuilding;

    public int UnitUpgrade;
    public int BuildingUpgrade;
}
