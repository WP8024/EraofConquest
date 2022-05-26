using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public BuildingBlueprint[] building;
    //BuildManager buildManager;

    public TextMeshProUGUI[] itemPriceText;
    public int[] itemPrice;
    //DataManager dataManager;

    private void Start()
    {
        //buildManager = BuildManager.instance;
        //dataManager = DataManager.instance;
    }

    public void SelectBuilding(int key)
    {
        Debug.Log("Select building : " + building[key]);
        BuildManager.Instance.SelectTurretToBuild(building[key]);
    }

    public void SelectUpgrade(int key)
    {
        Debug.Log("key : " + key);
        int price = itemPrice[key];
        if(price> DataManager.Instance.player.money)
        {    //구입실패
            StopCoroutine(Fail());
            StartCoroutine(Fail());
            return;
        }

        DataManager.Instance.player.money -= price;
        switch (key)
        {
            case 0:
                DataManager.Instance.player.up_MeleeUnit++;
                itemPrice[key] = DataManager.Instance.player.up_MeleeUnit * 1000;
                itemPriceText[key].text = itemPrice[key].ToString();
                break;
            case 1:
                Debug.Log("key 1 input");
                DataManager.Instance.player.up_RangeUnit++;
                itemPrice[key] = DataManager.Instance.player.up_RangeUnit * 1000;
                itemPriceText[key].text = itemPrice[key].ToString();
                break;
            case 2:
                DataManager.Instance.player.up_MagicUnit++;
                itemPrice[key] = DataManager.Instance.player.up_MagicUnit * 1000;
                itemPriceText[key].text = itemPrice[key].ToString();
                break;
            case 3:
                DataManager.Instance.player.up_CavalryUnit++;
                itemPrice[key] = DataManager.Instance.player.up_CavalryUnit * 1000;
                itemPriceText[key].text = itemPrice[key].ToString();
                break;
            case 4:
                DataManager.Instance.player.up_MaxUnit++;
                itemPrice[key] = DataManager.Instance.player.up_MaxUnit * 1000;
                itemPriceText[key].text = itemPrice[key].ToString();
                break;
            case 5:
                DataManager.Instance.player.up_MaxBuilding++;
                itemPrice[key] = DataManager.Instance.player.up_MaxBuilding * 1000;
                itemPriceText[key].text = itemPrice[key].ToString();
                break;
        }
        DataManager.Instance.Upgrade();

    }
    
    IEnumerator Fail()
    {
        yield return new WaitForSeconds(1f);

    }

}
