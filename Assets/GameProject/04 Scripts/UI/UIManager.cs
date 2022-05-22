using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    
     
    [SerializeField]
    private GameObject UnitUpgradPanel;
    [SerializeField]
    private GameObject ConstructPanel;
    [SerializeField]
    private GameObject OptionPanel;


    [SerializeField] public TextMeshProUGUI playerUnitText;
    [SerializeField] public TextMeshProUGUI playerBuildingText;
    [SerializeField] public TextMeshProUGUI playerMoneyText;
    [SerializeField] public TextMeshProUGUI enemyUnitText;
    [SerializeField] public TextMeshProUGUI enemyBuildingText;
    [SerializeField] public TextMeshProUGUI enemyMoneyText;
    PlayerData playerdata,enemydata;
  
 
    int curUnit, maxUnit, curBuilding, maxBuilding, money;

    
    private void Awake()
    {
        //playerUnitText = GetComponent<TextMeshProUGUI>();
        //playerBuildingText = GetComponent<TextMeshProUGUI>();
        //playerMoneyText = GetComponent<TextMeshProUGUI>();

        //enemyUnitText = GetComponent<TextMeshProUGUI>();
        //enemyBuildingText = GetComponent<TextMeshProUGUI>();
        //enemyMoneyText = GetComponent<TextMeshProUGUI>();


    }

    void Start()
    {
        ConstructPanel.SetActive(true);
        UnitUpgradPanel.SetActive(false);
       
        playerdata = DataManager.instance.player;
        enemydata = DataManager.instance.enemy;

    }
   

    public void Update()
    {
        Debug.Log(playerdata.curBuilding);
        Debug.Log("curBuilding log"+ playerdata.curBuilding);
        playerUnitText.text = playerdata.curUnit.ToString() + " / " + playerdata.maxUnit.ToString();
        playerBuildingText.text = playerdata.curBuilding.ToString() + " / " + playerdata.maxBuilding.ToString();
        playerMoneyText.text = playerdata.money.ToString();

        enemyUnitText.text = enemydata.curUnit.ToString() + " / " + enemydata.maxUnit.ToString();
        enemyBuildingText.text = enemydata.curBuilding.ToString() + " / " + enemydata.maxBuilding.ToString();
        enemyMoneyText.text = enemydata.money.ToString();
    }


    //Open whole settings menu when gear icon in top left corner is clicked
    public void openSettingsMenu()
    {
        Time.timeScale = 0;
        OptionPanel.SetActive(true);
    }
    //Close whole settings menu when arrow icon in top left corner is clicked
    public void closeSettingsMenu()
    {
        Time.timeScale = 1;
        OptionPanel.SetActive(false);
    }
    public void OnClickUpgradePanel()
    {
        ConstructPanel.SetActive(false);
        UnitUpgradPanel.SetActive(true);
    }

    public void OnClickConstructPanel()
    {
        ConstructPanel.SetActive(true);
        UnitUpgradPanel.SetActive(false);
    }
}
