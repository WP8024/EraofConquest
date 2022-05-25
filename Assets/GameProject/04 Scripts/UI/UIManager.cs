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
    //PlayerData playerdata,enemydata;
  
 
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
       
        //playerdata = DataManager.instance.player;
        //enemydata = DataManager.instance.enemy;

    }
   

    public void Update()
    {
        playerUnitText.text = DataManager.Instance.player.curUnit.ToString() + " / " + DataManager.Instance.player.maxUnit.ToString();
        playerBuildingText.text = DataManager.Instance.player.curBuilding.ToString() + " / " + DataManager.Instance.player.maxBuilding.ToString();
        playerMoneyText.text = DataManager.Instance.player.money.ToString();

        enemyUnitText.text = DataManager.Instance.enemy.curUnit.ToString() + " / " + DataManager.Instance.enemy.maxUnit.ToString();
        enemyBuildingText.text = DataManager.Instance.enemy.curBuilding.ToString() + " / " + DataManager.Instance.enemy.maxBuilding.ToString();
        enemyMoneyText.text = DataManager.Instance.enemy.money.ToString();
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
