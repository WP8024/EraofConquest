using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : MonoBehaviour
{
    public GameObject StartMenuPrefabs;

    /// �� ���ӿ�����Ʈ Map GameObject
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
        ///��Ȱ��ȭ activate Map
        StageSelectPrefabs.SetActive(true);
        ///��ŸƮ�޴� ��Ȱ��ȭ deactivate startmenu
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
        ///��Ȱ��ȭ activate Map
        StageSelectPrefabs.SetActive(true);


        ///��ŸƮ�޴� ��Ȱ��ȭ deactivate startmenu
        StartMenuPrefabs.SetActive(false);
    }

}
