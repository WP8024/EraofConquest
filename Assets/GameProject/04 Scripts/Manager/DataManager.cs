using System.Collections;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public string name;
    public int coin;
    public int level;

    public int currentUnit;
}


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    PlayerData curplayer = new PlayerData();
    

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