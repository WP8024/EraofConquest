using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    /// 메인클래스의 정적 싱글톤 변수 Singleton instance of main
    public static StageManager instance;
    private static int WIN = 1, LOSE = 0;

    /// 스타트 메뉴 게임오브젝트 Start menu gameObject
    public GameObject StartMenuPrefabs;

    /// 맵 게임오브젝트 Map GameObject
    public GameObject StageSelectPrefabs;

    private StageNode lastMapNodeBattleLoaded;

    // Start is called before the first frame update
    void Start()
    {
        ///메인에 할당 assign main
        instance = this;

        ///씬로드 listen for scene loaded
        SceneManager.sceneLoaded += OnSceneLoaded;

        ///맵 비활성화 disable map
        StageSelectPrefabs.SetActive(false);
     
    }

    /// 게임 시작 버튼클릭시 호출 Calld when start game button is clicked by player
    public void OnStartGameButtonClick()
    {


        ///맵활성화 activate Map
        StageSelectPrefabs.SetActive(true);
        ///스타트메뉴 비활성화 deactivate startmenu
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
        ///맵활성화 activate Map
        StageSelectPrefabs.SetActive(true);


        ///스타트메뉴 비활성화 deactivate startmenu
        StartMenuPrefabs.SetActive(false);
    }



    /// 배틀씬을 불러옴 Loads a new battle scene
    public void LoadNewBattle(StageNode stageNode)
    {
        ///맵노드 저장 store MapNode
        lastMapNodeBattleLoaded = stageNode;

        ///스테이지클래스 불러옴 Get Map
        Stage map = StageSelectPrefabs.GetComponent<Stage>();

        ///맵선택화면 숨김 hide map
        map.SetVisibility(false);

        ///배틀씬 불러옴 lode battle scene
        SceneManager.LoadScene(stageNode.sceneNameToLoad, LoadSceneMode.Additive);
    }


    /// 로딩이 끝나면 호출 Calld when scene finished loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ///씬 활성화 set to active scene
        SceneManager.SetActiveScene(scene);

        ///클래스들 초기화 Init classes
        //GameObject.Find("GameManager").GetComponent<GameManager>().Init();
        //GameObject.Find("CombatScripts").GetComponent<UIManager>().Init();
        //GameObject.Find("CombatScripts").GetComponent<PlayerInput>().Init();
        //GameObject.Find("CombatScripts").GetComponent<EnemyAI>().Init();

        ///전투ui 활성 show combat ui
        //CombatUI.instance.SetVisibility(true);
    }


    /// 씬로딩이 끝나는동안 코로틴을 사용해 대기 corutine for waiting for scene to finish loading
    IEnumerator waitForSceneLoad(string name)
    {
        while (SceneManager.GetActiveScene().name != name)
        {
            yield return null;
        }
    }


    /// 플레이어가 스테이지 전투를 마쳤을때 호출 Calld when player finished a MapNode battle
    public void OnBattleComplete(int battleResoult)
    {
        ///승리여부 확인 check if battleresoult was a win
        if (battleResoult == WIN)
        {
            ///지난 노드 완료로 tell MapNode that it got completed
            lastMapNodeBattleLoaded.OnNodeComplete();
        }

        ///맵클래스 불러오기 Get Map
        Stage map = StageSelectPrefabs.GetComponent<Stage>();

        /////스테이지 ui 숨김 hide combat ui
        //CombatUI.instance.SetVisibility(false);

        /////전투종료패널 숨김 hide end battle panel
        //CombatUI.instance.ShowEndBattlePanel(false);

        ///스테이지 선택 맵 활성화 show map
        map.SetVisibility(true);

        ///전투씬 비활성화 unload battle scene
        SceneManager.UnloadSceneAsync(lastMapNodeBattleLoaded.sceneNameToLoad);
    }
}
