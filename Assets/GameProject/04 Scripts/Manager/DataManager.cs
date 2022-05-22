using System.Collections;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    PlayerData curplayer = new PlayerData();

    [SerializeField]
    private int stageNumber;
    [SerializeField]
    public RTSDB rtsDB;

    public PlayerData player;
    public PlayerData enemy;

    public void SetUP()
    {
        int index = 0;
        for (int i = 0; i < rtsDB.PlayerDB.Count; ++i)
        {
            if (rtsDB.PlayerDB[i].playerID == 1)
            {
                player.playerID = rtsDB.PlayerDB[i].playerID;
                player.money = rtsDB.PlayerDB[i].money;
                player.maxUnit = rtsDB.PlayerDB[i].maxUnit;
                player.curUnit = rtsDB.PlayerDB[i].curUnit;
                player.maxBuilding = rtsDB.PlayerDB[i].maxBuilding;
                player.curBuilding = rtsDB.PlayerDB[i].curBuilding;
                player.up_MaxUnit = rtsDB.PlayerDB[i].up_MaxUnit;
                player.up_MaxBuilding = rtsDB.PlayerDB[i].up_MaxBuilding;
                player.up_MeleeUnit = rtsDB.PlayerDB[i].up_MeleeUnit;
                player.up_RangeUnit = rtsDB.PlayerDB[i].up_RangeUnit;
                player.up_MagicUnit = rtsDB.PlayerDB[i].up_MagicUnit;
                player.up_CavalryUnit = rtsDB.PlayerDB[i].up_CavalryUnit;
            }

            if (rtsDB.PlayerDB[i].playerID == stageNumber)
            {
                enemy.playerID = rtsDB.PlayerDB[i].playerID;
                enemy.money = rtsDB.PlayerDB[i].money;
                enemy.maxUnit = rtsDB.PlayerDB[i].maxUnit;
                enemy.curUnit = rtsDB.PlayerDB[i].curUnit;
                enemy.maxBuilding = rtsDB.PlayerDB[i].maxBuilding;
                enemy.curBuilding = rtsDB.PlayerDB[i].curBuilding;
                enemy.up_MaxUnit = rtsDB.PlayerDB[i].up_MaxUnit;
                enemy.up_MaxBuilding = rtsDB.PlayerDB[i].up_MaxBuilding;
                enemy.up_MeleeUnit = rtsDB.PlayerDB[i].up_MeleeUnit;
                enemy.up_RangeUnit = rtsDB.PlayerDB[i].up_RangeUnit;
                enemy.up_MagicUnit = rtsDB.PlayerDB[i].up_MagicUnit;
                enemy.up_CavalryUnit = rtsDB.PlayerDB[i].up_CavalryUnit;
            }
        }

    }

    public void Upgrade()
    {
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        SetUP();
        //씬이 바뀌어도 사라지면 안되기때문에 이동
        DontDestroyOnLoad(this.gameObject);
    }


    /// <summary>
    /// 데이터 클래스를 저장
    /// 그데이터를 json으로 변환
    /// 제이슨을 외부에 저장
    /// </summary>
    [ContextMenu("To Json Data")]
    void SaveDataToJson()
    {
        //string jsonData = JsonUtility.ToJson(unitdata); // 한줄로만 저장되어 사람이 읽기어려움
        string jsonData= JsonUtility.ToJson(curplayer,true); // printPrint적용 으로 사람이 읽기 편하게 구획별로 나눠줌
        string path = Application.dataPath + ("/data.json"); //datapath는 현재 실행중인 유니티 프로젝트 경로
    }
    /// <summary>
    /// 외부에 저장된 json을 가져옴
    /// json을 데이터 클래스형태로 변환
    /// 불러온 데이터를 사용
    /// </summary>
    [ContextMenu("From Json Data")]
    void LoadDataToJson()
    {
        //string path = Application.dataPath + ("/data.json"); //dataPath에 + string값으로 폴더/파일저장
        string path = Path.Combine(Application.dataPath,"/data.json"); //위와 같은 방식이지만 os구분없이 기능하나로 묶여있음
        string jsonData = File.ReadAllText(path);//해당경로의 모든 문자열을 데이터를 읽어 string으로 가져옴

        JsonUtility.FromJson<PlayerData>(jsonData);// 
    }
}