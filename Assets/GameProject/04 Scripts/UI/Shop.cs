using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public BuildingBlueprint[] building;
    BuildManager buildManager;

    public int[] itemPrice;
    DataManager dataManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        dataManager = DataManager.instance;
    }

    public void SelectBuilding(int key)
    {
        Debug.Log("Select building : " + building[key]);
        buildManager.SelectTurretToBuild(building[key]);
    }

    public void SelectUpgrade(int key)
    {
        Debug.Log("key : " + key);
        int price = itemPrice[key];
        if(price> dataManager.player.money)
        {    //구입실패
            StopCoroutine(Fail());
            StartCoroutine(Fail());
            return;
        }

        dataManager.player.money -= price;
        switch (key)
        {
            case 0:
                dataManager.player.up_MeleeUnit++;
                itemPrice[key] = dataManager.player.up_MeleeUnit * 1000;
                break;
            case 1:
                Debug.Log("key 1 input");
                dataManager.player.up_RangeUnit++;
                itemPrice[key] = dataManager.player.up_RangeUnit * 1000;
                break;
            case 2:
                dataManager.player.up_MagicUnit++;
                itemPrice[key] = dataManager.player.up_MagicUnit * 1000;
                break;
            case 3:
                dataManager.player.up_CavalryUnit++;
                itemPrice[key] = dataManager.player.up_CavalryUnit * 1000;
                break;
            case 4:
                dataManager.player.up_MaxUnit++;
                itemPrice[key] = dataManager.player.up_MaxUnit * 1000;
                break;
            case 5:
                dataManager.player.up_MaxBuilding++;
                itemPrice[key] = dataManager.player.up_MaxBuilding * 1000;
                break;
        }
        dataManager.Upgrade();

    }
    
    IEnumerator Fail()
    {
        yield return new WaitForSeconds(1f);

    }

}
