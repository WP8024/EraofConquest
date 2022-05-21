using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    /// ����Ŭ������ ���� �̱��� ���� Singleton instance of main
    public static StageManager instance;
    private static int WIN = 1, LOSE = 0;

    /// ��ŸƮ �޴� ���ӿ�����Ʈ Start menu gameObject
    public GameObject StartMenuPrefabs;

    /// �� ���ӿ�����Ʈ Map GameObject
    public GameObject StageSelectPrefabs;

    private StageNode lastMapNodeBattleLoaded;

    // Start is called before the first frame update
    void Start()
    {
        ///���ο� �Ҵ� assign main
        instance = this;

        ///���ε� listen for scene loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        ///�� ��Ȱ��ȭ disable map
        StageSelectPrefabs.SetActive(false);
     
    }

    /// ���� ���� ��ưŬ���� ȣ�� Calld when start game button is clicked by player
    public void OnStartGameButtonClick()
    {


        ///��Ȱ��ȭ activate Map
        StageSelectPrefabs.SetActive(true);
        ///��ŸƮ�޴� ��Ȱ��ȭ deactivate startmenu
        StartMenuPrefabs.SetActive(false);
    }
    public void OnOptionButtonClick()
    {

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



    /// ��Ʋ���� �ҷ��� Loads a new battle scene
    public void LoadNewBattle(StageNode stageNode)
    {
        ///�ʳ�� ���� store MapNode
        lastMapNodeBattleLoaded = stageNode;

        ///��������Ŭ���� �ҷ��� Get Map
        Stage map = StageSelectPrefabs.GetComponent<Stage>();

        ///�ʼ���ȭ�� ���� hide map
        map.SetVisibility(false);

        ///��Ʋ�� �ҷ��� lode battle scene
        SceneManager.LoadScene(stageNode.sceneNameToLoad, LoadSceneMode.Additive);
    }


    /// �ε��� ������ ȣ�� Calld when scene finished loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ///�� Ȱ��ȭ set to active scene
        SceneManager.SetActiveScene(scene);

        ///Ŭ������ �ʱ�ȭ Init classes
        //GameObject.Find("GameManager").GetComponent<GameManager>().Init();
        //GameObject.Find("CombatScripts").GetComponent<UIManager>().Init();
        //GameObject.Find("CombatScripts").GetComponent<PlayerInput>().Init();
        //GameObject.Find("CombatScripts").GetComponent<EnemyAI>().Init();

        ///����ui Ȱ�� show combat ui
        //CombatUI.instance.SetVisibility(true);
    }


    /// ���ε��� �����µ��� �ڷ�ƾ�� ����� ��� corutine for waiting for scene to finish loading
    IEnumerator waitForSceneLoad(string name)
    {
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
        }
    }


    /// �÷��̾ �������� ������ �������� ȣ�� Calld when player finished a MapNode battle
    public void OnBattleComplete(int battleResoult)
    {
        ///�¸����� Ȯ�� check if battleresoult was a win
        if (battleResoult == WIN)
        {
            ///���� ��� �Ϸ�� tell MapNode that it got completed
            lastMapNodeBattleLoaded.OnNodeComplete();
        }

        ///��Ŭ���� �ҷ����� Get Map
        Stage map = StageSelectPrefabs.GetComponent<Stage>();

        /////�������� ui ���� hide combat ui
        //CombatUI.instance.SetVisibility(false);

        /////���������г� ���� hide end battle panel
        //CombatUI.instance.ShowEndBattlePanel(false);

        ///�������� ���� �� Ȱ��ȭ show map
        map.SetVisibility(true);

        ///������ ��Ȱ��ȭ unload battle scene
        SceneManager.UnloadSceneAsync(lastMapNodeBattleLoaded.sceneNameToLoad);
    }
}
