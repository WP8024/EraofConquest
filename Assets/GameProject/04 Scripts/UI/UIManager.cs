using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private PlayerStats stats;
     
    [SerializeField]
    private GameObject UnitUpgradPanel;
    [SerializeField]
    private GameObject ConstructPanel;
    [SerializeField]
    private GameObject OptionPanel;


    [SerializeField] private TextMeshProUGUI playerUnit;
    [SerializeField] private TextMeshProUGUI playerBuilding;
    [SerializeField] private TextMeshProUGUI playerMoney;

    void Start()
    {
        ConstructPanel.SetActive(true);
        UnitUpgradPanel.SetActive(false);


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
