using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public GameObject StartMenuPrefabs;

    /// 맵 게임오브젝트 Map GameObject
    public GameObject StageSelectPrefabs;

    public GameObject OptionMenuPrefabs;

    private StageNode lastMapNodeBattleLoaded;

    bool OptionActive = false;
    // Start is called before the first frame update
    void Start()
    {
        StageSelectPrefabs.SetActive(false);
    }

    public void OnStartGameButtonClick()
    {
        ///맵활성화 activate Map
        StageSelectPrefabs.SetActive(true);
        ///스타트메뉴 비활성화 deactivate startmenu
        StartMenuPrefabs.SetActive(false);
        OptionMenuPrefabs.SetActive(false);
    }

    public void OnOptionButtonClick()
    {
        OptionActive = !OptionActive;
        OptionMenuPrefabs.SetActive(OptionActive);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }

    public void OnStageSelectButtonClick()
    {
        ///맵활성화 activate Map
        StageSelectPrefabs.SetActive(true);


        ///스타트메뉴 비활성화 deactivate startmenu
        StartMenuPrefabs.SetActive(false);
    }

}
